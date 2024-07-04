using VB.API.Models;
using VB.Data.Models;

namespace VB.API.Logic
{
    public interface IDatabaseQueryService
    {
        Task AddSearchRequestRecord(RequestProperties requiredProperties);

        Task<List<FilmRequest>> GetAllFilmRequests();

        Task<FilmRequest> GetFilmRequestByFilmName(string filmName);

        Task<List<FilmRequest>> GetFilmRequestsForDatePeriod(string startDate, string endDate);

        Task<FilmRequestAggregateResponse> GetFilmRequestAggregatedDataForDate(string searchDate);

        Task DeleteFilmRequestByFilmName(string filmName);
    }
}
