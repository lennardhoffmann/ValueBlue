using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VB.DataAccess.Models;

namespace VB.DataAccess.Repositories
{
    public interface IFilmRequestRepository
    {
        Task AddSearchRequestAsync();

        Task<List<string>> GetAllSearchRequestsAsync();

        Task<FilmRequest> GetSearchRequestBySearchTokenAsync(string tokenName);

        Task DeleteSearchRequestBtSearchTokenAsync(string tokenName);
    }
}
