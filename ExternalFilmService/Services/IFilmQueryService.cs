using ExternalFilmService.Models;

namespace ExternalFilmService.Services
{
    public interface IFilmQueryService
    {
         Task<FilmServiceResponse> GetFilmByName(string filmName);
    }
}
