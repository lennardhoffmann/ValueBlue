using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using VB.Data.Models;

namespace VB.Data
{
    public class MongoDBContext : DbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDBContext(DbContextOptions<MongoDBContext> dbContextOptions, IMongoDatabase database) : base(dbContextOptions)
        {
            _database = database;
        }

        public DbSet<FilmRequest> FilmRequests { get; set; }

        public IMongoCollection<FilmRequest> FilmRequestsCollection => _database.GetCollection<FilmRequest>(nameof(FilmRequests));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FilmRequest>().ToCollection("filmRequests");
        }
    }
}
