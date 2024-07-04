using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Diagnostics;
using VB.Data.Models;

namespace VB.Data.Repositories
{
    public class FilmRequestRepository : IFilmRequestRepository
    {
        private readonly IMongoCollection<FilmRequest> _collection;
        public FilmRequestRepository(IMongoDatabase mongoDatabase)
        {
            _collection = mongoDatabase.GetCollection<FilmRequest>(nameof(FilmRequest));
        }

        public async Task<bool> AddSearchRequestAsync(FilmRequest requestRecord)
        {
            await _collection.InsertOneAsync(requestRecord);

            return true;
        }

        public async Task<List<FilmRequest>> GetAllSearchRequestsAsync()
        {
            var filmRequests = await _collection.Find(Builders<FilmRequest>.Filter.Empty).ToListAsync();

            return filmRequests;
        }

        public async Task<FilmRequest> GetSearchRequestBySearchTokenAsync(string tokenName)
        {
            var filmRequest = await _collection.Find(x => x.Search_Token.ToLower() == tokenName.ToLower()).FirstOrDefaultAsync();
            return filmRequest;
        }

        public async Task<List<FilmRequest>> GetSearchRequestByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var filter = Builders<FilmRequest>.Filter.Gte(x => x.TimeStamp, startDate) &
                         Builders<FilmRequest>.Filter.Lt(x => x.TimeStamp, endDate.AddDays(1));

            var filmRequests = await _collection.Find(filter).ToListAsync();
            return filmRequests;
        }

        public async Task<FilmRequestAggregateResponse> GetAggregateSearchDataForDateAsync(DateTime searchDate)
        {
            // Construct the filter for the aggregation pipeline
            var filter = Builders<FilmRequest>.Filter.Gt(x => x.TimeStamp, searchDate.ToUniversalTime().Date) &
                         Builders<FilmRequest>.Filter.Lte(x => x.TimeStamp, searchDate.ToUniversalTime().Date.AddDays(2));

            var filterDoc = filter.Render(BsonSerializer.SerializerRegistry.GetSerializer<FilmRequest>(), BsonSerializer.SerializerRegistry);

            // Match stage for aggregation pipeline
            var matchStage = new BsonDocument("$match", filterDoc);

            // Group stage to count occurrences of each Search_Token
            var groupStage = new BsonDocument
            {
                { "$group", new BsonDocument
                    {
                        { "_id", "$search_Token" },  // Group by movie title
                        { "count", new BsonDocument("$sum", 1) }  // Count occurrences of each movie title
                    }
                }
            };

            // Aggregation pipeline
            var pipeline = new[] { matchStage, groupStage };

            // Log the pipeline to inspect before execution (optional)
            Console.WriteLine($"Aggregation Pipeline: {pipeline.ToJson()}");

            try
            {
                // Execute aggregation
                var cursor = await _collection.AggregateAsync<BsonDocument>(pipeline);
                var results = await cursor.ToListAsync();

                // Log the results count
                Console.WriteLine($"Aggregation Results Count: {results.Count}");

                // Convert the aggregation results to a dictionary
                var mostFrequentSearchTokens = results.ToDictionary(
                    doc => doc["_id"].AsString,
                    doc => doc["count"].AsInt32
                );

                // Retrieve films for the specified date
                var filmsForDate = await _collection.Find(filter).ToListAsync();

                var aggregateResult = new FilmRequestAggregateResponse
                {
                    TotalRequests = filmsForDate.Count, // Use the count from the Find operation
                    MostFrequentSearchTokens = mostFrequentSearchTokens
                };

                return aggregateResult;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing aggregation pipeline: {ex.Message}");
                throw; // Rethrow or handle the exception as appropriate
            }
        }

        public async Task<bool> DeleteSearchRequestBtSearchTokenAsync(string tokenName)
        {
            await _collection.DeleteOneAsync(x => x.Search_Token.ToLower() == tokenName.ToLower());

            return true;

        }


    }
}

public class SearchTokenCount
{
    public string Search_Token { get; set; }
    public int Count { get; set; }
}
