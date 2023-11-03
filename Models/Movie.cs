using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieReview.Models{

    public class MovieCard{
        public Movie? movie {get; set;}
        public Api? api {get;set;}
}
    public class Movie{

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("imdbId")]
        public string? imdbID { get; set; }

        [BsonElement("title")]
        public string? Title { get; set; }
        
        [BsonElement("poster")]  
        public string? Poster { get; set; }

        [BsonElement("plot")]
        public string? Plot { get; set; }        
        [BsonElement("Year")]
        public int Year { get; set; }  
        [BsonElement("genre")]      
        public List<string>? Genre { get; set; }
        [BsonElement("director")]
        public string? Director { get; set; }
        [BsonElement("actors")]
        public string? Actors { get; set; }

        [BsonElement("Rated")]
        public string? Rated { get; set; }
    }

    public class Api
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("title")]
        public string? Title { get; set; }
        [BsonElement("year")]
        public string? Year { get; set; }
        [BsonElement("rated")]
        public string? Rated { get; set; }
        [BsonElement("released")]
        public string? Released { get; set; }
        [BsonElement("runtime")]
        public string? Runtime { get; set; }
        [BsonElement("genre")]
        public string? Genre { get; set; }
        [BsonElement("director")]
        public string? Director { get; set; }
        [BsonElement("writer")]
        public string? Writer { get; set; }
        [BsonElement("actor")]
        public string? Actors { get; set; }
        [BsonElement("plot")]
        public string? Plot { get; set; }
        [BsonElement("language")]
        public string? Language { get; set; }
        [BsonElement("country")]
        public string? Country { get; set; }
        [BsonElement("awards")]
        public string? Awards { get; set; }
        [BsonElement("poster")]
        public string? Poster { get; set; }
        [BsonElement("ratings")]
        public List<Rating>? Ratings { get; set; }
        [BsonElement("metascore")]
        public string? Metascore { get; set; }
        [BsonElement("imdbratings")]
        public string? imdbRating { get; set; }
        [BsonElement("imdbvotes")]
        public string? imdbVotes { get; set; }
        [BsonElement("imdbid")]
        public string? imdbID { get; set; }
        [BsonElement("type")]
        public string? Type { get; set; }
        [BsonElement("dvd")]
        public string? DVD { get; set; }
        [BsonElement("boxoffice")]
        public string? BoxOffice { get; set; }
        [BsonElement("production")]
        public string? Production { get; set; }
        [BsonElement("website")]
        public string? Website { get; set; }
        [BsonElement("response")]
        public string? Response { get; set; }
        [BsonElement("review")]
        public List<Review>? Comments{ get; set; }
        
    }
    public class Rating
    {
        public string? Source { get; set; }
        public string? Value { get; set; }
    }
    }

 