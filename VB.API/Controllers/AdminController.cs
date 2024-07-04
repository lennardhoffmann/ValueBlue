using Microsoft.AspNetCore.Mvc;
using VB.API.Authorization;
using VB.API.Logic;
using VB.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class AdminController : ControllerBase
    {
        private readonly IDatabaseQueryService _queryService;

        /// <summary>
        ///The controller for retrieving and deleting film request records from the database.
        /// </summary>
        public AdminController(IDatabaseQueryService queryService)
        {
            _queryService = queryService;
        }

        /// <summary>
        /// Retrieves all request that have been made.
        /// </summary>
        [HttpGet("films")]
        public async Task<List<FilmRequest>> GetAllFilmRequests()
        {
            var filmRequests = await _queryService.GetAllFilmRequests();
            return filmRequests;
        }

        /// <summary>
        /// Retrieves a request of a specific movie name from the database.
        /// </summary>
        /// <param name="filmName">The name of the film to be retrieved from the collection</param>
        [HttpGet("{filmName}")]
        public async Task<IActionResult> GetFilmRequestByName(string filmName)
        {
            var filmRequest = await _queryService.GetFilmRequestByFilmName(filmName);
            return Ok(filmRequest);
        }

        /// <summary>
        /// Retrieves data within the specified date range.
        /// </summary>
        /// <param name="startDate">The start date in the format YYYY-MM-DD</param>
        /// <param name="endDate">The end date in the format YYYY-MM-DD</param>
        [HttpGet("&startDate={startDate}&endate={endDate}")]
        public async Task<IActionResult> GetFilmRequestByDateRange(string startDate, string endDate)
        {

            var filmRequests = await _queryService.GetFilmRequestsForDatePeriod(startDate, endDate);

            return Ok(filmRequests);
        }

        /// <summary>
        /// Retrieves aggregate data for the specified date.
        /// </summary>
        /// <param name="searchDate">The start date in the format YYYY-MM-DD</param>
        [HttpGet("aggregate/{searchDate}")]
        public async Task<IActionResult> GetFilmAggregateDataByDate(string searchDate)
        {
            var result = await _queryService.GetFilmRequestAggregatedDataForDate(searchDate);


            return Ok(result);
        }

        /// <summary>
        /// Deletes a request of a specific movie name from the database.
        /// </summary>
        /// <param name="filmName">The name of the film to be removed from the collection</param>
        [HttpDelete("{filmName}")]
        public async Task<IActionResult> DeleteFilmByname(string filmName)
        {
            await _queryService.DeleteFilmRequestByFilmName(filmName);

            return Ok("Film request successfully deleted");
        }
    }
}
