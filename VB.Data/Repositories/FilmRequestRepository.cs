using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VB.Data.Models;

namespace VB.Data.Repositories
{
    public class FilmRequestRepository : IFilmRequestRepository
    {
        private readonly MongoDBContext _context;
        public FilmRequestRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSearchRequestAsync(FilmRequest requestRecord)
        {
            await _context.FilmRequests.AddAsync(requestRecord);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<FilmRequest>> GetAllSearchRequestsAsync()
        {
            var filmRequests = await _context.FilmRequests.ToListAsync();

            return filmRequests;
        }

        public async Task<FilmRequest> GetSearchRequestBySearchTokenAsync(string tokenName)
        {
            var filmRequest = await _context.FilmRequests.Where(x => x.Search_Token.ToLower() == tokenName.ToLower()).FirstOrDefaultAsync();
            return filmRequest;
        }

        public async Task<List<FilmRequest>> GetSearchRequestByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var filmRequests = await _context.FilmRequests.Where(x => x.TimeStamp.Date >= startDate.Date && x.TimeStamp.Date <= endDate.Date).ToListAsync();

            return filmRequests;
        }

        public async Task GetAggregateSearchDataForDateAsync(DateTime searchdate)
        {

            //    var filter = Builders<FilmSearch>.Filter.Gte(f => f.TimeStamp, specificDate.Date) &
            //            Builders<FilmSearch>.Filter.Lt(f => f.TimeStamp, specificDate.Date.AddDays(1));

            //    var group = new BsonDocument
            //{
            //    { "_id", "$search_Token" },
            //    { "count", new BsonDocument("$sum", 1) },
            //    { "averageProcessingTime", new BsonDocument("$avg", "$processing_Time_Ms") }
            //};

            //    var sort = new BsonDocument("count", -1);

            //    var pipeline = new BsonDocument[]
            //    {
            //    new BsonDocument("$match", filter),
            //    new BsonDocument("$group", group),
            //    new BsonDocument("$sort", sort)
            //    };

            //    var result = await _filmSearchCollection.AggregateAsync<BsonDocument>(pipeline).ToListAsync();

            return;
        }

        public async Task<bool> DeleteSearchRequestBtSearchTokenAsync(string tokenName)
        {
            var filmRequest = await _context.FilmRequests.Where(x => x.Search_Token.ToLower().Equals(tokenName.ToLower())).FirstOrDefaultAsync();
            if (filmRequest == null)
            {
                return false;
            }

            _context.FilmRequests.Remove(filmRequest);
            await _context.SaveChangesAsync();

            return true;

        }


    }
}
