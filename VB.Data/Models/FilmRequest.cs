using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace VB.Data.Models
{
    [Collection("filmRequests")]
    public class FilmRequest
    {
        public ObjectId _id { get; set; }
        public string Search_Token { get; set; }
        public string imdbID { get; set; }
        public long Processing_Time_Ms { get; set; }
        public DateTime TimeStamp { get; set; }
        public string IP_Address { get; set; }
    }
}
