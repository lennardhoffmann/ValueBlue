using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB.OMDBService.Services
{
    public interface IFilmQueryService
    {
        Task<string> GetFilmByName(string filmName);
    }
}
