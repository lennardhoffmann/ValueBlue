using ExternalFilmService.Models;

namespace ExternalFilmService.Services
{
    public interface IExternalFilmQueryService
    {
         Task<FilmServiceResponse> GetFilmByName(string filmName);
    }
}
