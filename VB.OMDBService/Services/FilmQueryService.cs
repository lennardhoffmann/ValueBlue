using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB.OMDBService.Services
{
    public class FilmQueryService: IFilmQueryService
    {

        public async Task<string> GetFilmByName(string filmName)
        {
            string url = "{URL}/?apikey={ApiKey}&s={filmName}";

            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();

            //var response = await client.GetAsync(url);
            return "";
        }
    }
}
