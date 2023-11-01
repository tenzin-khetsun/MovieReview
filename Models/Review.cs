using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieReview.Models{
      
    public class Review{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReviewId { get; set; }
        [BsonElement("imdbId")]
        public string? imdbID { get; set; }
        [BsonElement("User name")]
        public string? ReviewUser { get; set; }
        [BsonElement("Ratings")]
        public int Ratings { get; set; }
        [BsonElement("Content")]
        public string? Content { get; set; }
        [BsonElement("Date time")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class ViewPage{
        public Movie? movies { get; set; }
        public List<Review>? reviews { get; set; }
        public User? user {get; set;}
    }
}