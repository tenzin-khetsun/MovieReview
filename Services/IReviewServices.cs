using Microsoft.AspNetCore.Mvc;
using MovieReview.Models;

namespace MovieReview.Services{
    public interface IReview{
        // public Review GetReviewsMethod();  
        public Review CreateReview(Review review);
        public bool DeleteReview(string reviewId);
    }
}