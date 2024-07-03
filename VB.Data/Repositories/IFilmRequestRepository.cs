using VB.Data.Models;

namespace VB.Data.Repositories
{
    public interface IFilmRequestRepository
    {
        Task<bool> AddSearchRequestAsync(FilmRequest requestRecord);

        Task<List<FilmRequest>> GetAllSearchRequestsAsync();

        Task<FilmRequest> GetSearchRequestBySearchTokenAsync(string tokenName);

        Task<List<FilmRequest>> GetSearchRequestByDateRangeAsync(DateTime startDate, DateTime endDate);

        Task GetAggregateSearchDataForDateAsync(DateTime searchdate);

        Task<bool> DeleteSearchRequestBtSearchTokenAsync(string tokenName);
    }
}
