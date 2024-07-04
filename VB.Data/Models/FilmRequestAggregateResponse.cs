using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VB.Data.Models
{
    public class FilmRequestAggregateResponse
    {
        public int TotalRequests { get; set; }
        public Dictionary<string, int>? MostFrequentSearchTokens { get; set; }
    }
}
