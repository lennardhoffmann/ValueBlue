using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VB.Data.Models
{
    public class FilmRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("search_Token")]
        public string Search_Token { get; set; }

        [BsonElement("imdbID")]
        public string imdbID { get; set; }

        [BsonElement("processing_Time_Ms")]
        public long Processing_Time_Ms { get; set; }

        [BsonElement("timeStamp")]
        public DateTime TimeStamp { get; set; }

        [BsonElement("iP_Address")]
        public string IP_Address { get; set; }
    }
}
