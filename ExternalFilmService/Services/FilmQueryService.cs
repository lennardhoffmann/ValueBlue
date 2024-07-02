using ExternalFilmService.Models;
using Newtonsoft.Json;

namespace ExternalFilmService.Services
{
    public class FilmQueryService : IFilmQueryService
    {
        private readonly string _url;
        private readonly string? _apiKey;

        public FilmQueryService(FilmServiceProperties properties)
        {
            _url = properties.BaseUrl;
            _apiKey = properties.ApiKey;
        }
        public async Task<FilmServiceResponse> GetFilmByName(string filmName)
        {
            try
            {
                var url = $"{_url}?apikey={_apiKey}&t={filmName}";

                using HttpClient client = new();
                client.DefaultRequestHeaders.Accept.Clear();

                var result = await client.GetAsync(url);
                var jsonConent = await result.Content.ReadAsStringAsync();
                var filmDetails = JsonConvert.DeserializeObject<FilmServiceResponse>(jsonConent);
                if (!VerifyFilmNameAndReleaseExists(filmDetails))
                {
                    throw new Exception("Exception thrown");
                }

                return filmDetails;

            }
            catch (Exception e)
            {

                throw;
            }
        }

        private bool VerifyFilmNameAndReleaseExists(FilmServiceResponse filmDetails)
        {
            return (!string.IsNullOrEmpty(filmDetails.Title) && !string.IsNullOrEmpty(filmDetails.Released));
        }
    }
}
