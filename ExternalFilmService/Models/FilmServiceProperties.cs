using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalFilmService.Models
{
    public class FilmServiceProperties
    {
        public required string ServiceName { get; set; } 
        public required string BaseUrl { get; set; } 
        public string ApiKey { get; set; }
    }
}
