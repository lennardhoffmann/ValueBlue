using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VB.DataAccess.Models;

namespace VB.DataAccess.Repositories
{
    public class FilmRequestRepository:IFilmRequestRepository
    {
        private readonly MongoDBContext _context;
        public FilmRequestRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task AddSearchRequestAsync()
        {
            return;
        }

        public async Task<List<string>> GetAllSearchRequestsAsync()
        {
            return null;
        }

        public async Task<FilmRequest> GetSearchRequestBySearchTokenAsync(string tokenName)
        {
            return null;
        }

        public async Task DeleteSearchRequestBtSearchTokenAsync(string tokenName)
        {
            return;
        }


    }
}
