using ExternalFilmService.Models;
using ExternalFilmService.Services;
using Microsoft.AspNetCore.Mvc;
using VB.API.Logic;
using VB.API.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IDatabaseQueryService _queryService;
        public SearchController(IConfiguration configuration, IDatabaseQueryService queryService)
        {
            _configuration = configuration;
            _queryService = queryService;
        }

        /// <summary>
        /// Retrieves data of a specific movie name from the external service.
        /// </summary>
        /// <param name="filmTitle">The name of the film to retrieve data for from the external service</param>
        [HttpGet("{filmTitle}")]
        public async Task<IActionResult> GetFilmData(string filmTitle)
        {
            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
            var defaultFilmServiceProvider = _configuration.GetSection("DefaultFilmServiceProvider").Value;
            var filmProviderProperties = _configuration.GetSection("FilmServiceProviders").Get<List<FilmServiceProperties>>();
            var defaultFilmProviderProperties = (filmProviderProperties?.Find(x => x.ServiceName == defaultFilmServiceProvider)) ?? throw new Exception("No default film service provider options configured");

            var filmService = new ExternalFilmQueryService(new HttpClient(), defaultFilmProviderProperties);
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var filmDetails = await filmService.GetFilmByName(filmTitle) ?? throw new Exception();
            watch.Stop();

            var requestRequiredProperties = new RequestProperties
            {
                FilmName = filmDetails.Title,
                RequestResponseTime = watch.ElapsedMilliseconds,
                ImdbID = filmDetails.imdbID,
                UserIpAddress = ipAddress,
                TimeStamp = DateTime.Now
            };

            await _queryService.AddSearchRequestRecord(requestRequiredProperties);

            return Ok(filmDetails);

        }
    }
}
