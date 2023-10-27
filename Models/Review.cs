using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models{
      
    public class Review{
        public int ReviewId { get; set; }
        public string? imdbID { get; set; }
        public User? ReviewUser { get; set; }
        public int Ratings { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}