using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models{
    public class Movie{
        public int Id { get; set; }
        public string? Title { get; set; }        
        public string? Plot { get; set; }        
        public DateTime Year { get; set; }        
        public string? Genre { get; set; }
        public string? Director { get; set; }
        public string? Actors { get; set; }
        public string? IMDBRating { get; set; }
        public string? Rated { get; set; }


    }
}