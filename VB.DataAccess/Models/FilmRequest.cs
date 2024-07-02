using MongoDB.Bson;
using MongoDB.EntityFrameworkCore;

namespace VB.DataAccess.Models
{
    [Collection("filmRequests")]
    public class FilmRequest
    {
        public ObjectId _id { get; set; }
        public string Search_Token { get; set; }
        public string ImdbId { get; set; }
        public long Processing_Time_Ms { get; set; }
        public DateTime TimeStamp { get; set; }
        public string IP_Address { get; set; }
    }
}
