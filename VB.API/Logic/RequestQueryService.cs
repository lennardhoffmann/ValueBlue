using AutoMapper;
using System.Globalization;
using VB.API.Models;
using VB.Data.Models;
using VB.Data.Repositories;

namespace VB.API.Logic
{
    public class RequestQueryService : IRequestQueryService
    {
        private readonly IMapper _mapper;
        private readonly IFilmRequestRepository _repository;
        public RequestQueryService(IMapper mapper, IFilmRequestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task AddSearchRequestRecord(RequestProperties requiredProperties)
        {
            var filmRequestRecord = _mapper.Map<FilmRequest>(requiredProperties);
            var result = await _repository.AddSearchRequestAsync(filmRequestRecord);
            if (result && result != null)
            {
                return;
            }
            throw new Exception("Could not add record");
        }

        public async Task<List<FilmRequest>> GetAllFilmRequests()
        {
            var filmRequests = await _repository.GetAllSearchRequestsAsync();
            return filmRequests;
        }

        public async Task<FilmRequest> GetFilmRequestByFilmName(string filmName)
        {
            var filmRequest = await _repository.GetSearchRequestBySearchTokenAsync(filmName);
            if (filmRequest == null)
            {
                throw new Exception($"Film with title {filmName} not found");
            }

            return filmRequest;
        }

        public async Task<List<FilmRequest>> GetFilmRequestsForDatePeriod(string startDate, string endDate)
        {
            DateTime periodStartDate;
            bool successStartDate = DateTime.TryParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out periodStartDate);
            if (!successStartDate)
            {
                throw new BadHttpRequestException("Invalid format for start date provided");
            }

            DateTime periodEndDate;
            bool successEndDate = DateTime.TryParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out periodEndDate);
            if (!successEndDate)
            {
                throw new BadHttpRequestException("Invalid format for end date provided");
            }

            var result = await _repository.GetSearchRequestByDateRangeAsync(periodStartDate, periodEndDate);
            if (result == null)
            {
                throw new Exception();
            }

            return result;
        }

        public async Task DeleteFilmRequestByFilmName(string filmName)
        {
            var result = await _repository.DeleteSearchRequestBtSearchTokenAsync(filmName);
            if (!result)
            {
                throw new Exception("Record could not be deleted");
            }
        }
    }
}
