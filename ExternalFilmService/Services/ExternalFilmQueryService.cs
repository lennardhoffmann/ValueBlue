using ExternalFilmService.Models;
using Newtonsoft.Json;

namespace ExternalFilmService.Services
{
    public class ExternalFilmQueryService : IExternalFilmQueryService
    {
        private readonly string _url;
        private readonly string? _apiKey;
        private readonly HttpClient _httpClient;

        public ExternalFilmQueryService(HttpClient client, FilmServiceProperties properties)
        {
            _url = properties.BaseUrl;
            _apiKey = properties.ApiKey;
            _httpClient = client;
        }
        public async Task<FilmServiceResponse> GetFilmByName(string filmName)
        {
            if (string.IsNullOrEmpty(filmName))
            {
                throw new ArgumentNullException(nameof(filmName));
            }

            var url = $"{_url}?apikey={_apiKey}&t={filmName}";
            _httpClient.DefaultRequestHeaders.Accept.Clear();

            var result = await _httpClient.GetAsync(url);
            var jsonConent = await result.Content.ReadAsStringAsync();
            var filmDetails = JsonConvert.DeserializeObject<FilmServiceResponse>(jsonConent);
            if (!VerifyFilmNameAndReleaseExists(filmDetails))
            {
                throw new Exception("Film does not seem to exist");
            }

            return filmDetails;
        }

        private static bool VerifyFilmNameAndReleaseExists(FilmServiceResponse filmDetails)
        {
            return (!string.IsNullOrEmpty(filmDetails.Title) && !string.IsNullOrEmpty(filmDetails.Released));
        }
    }
}
