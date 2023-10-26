using System.ComponentModel.DataAnnotations;

namespace MovieReview.Models{
    public class Review{
        public int Id { get; set; }
        public User? ReviewUser { get; set; }
        [Range(1,5, ErrorMessage ="The rating must be between 1 and 5")]
        public int Ratings { get; set; }

        [MaxLength(500, ErrorMessage ="Minimum 200 characters")]
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}