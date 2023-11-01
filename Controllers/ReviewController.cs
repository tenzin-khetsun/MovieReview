using Microsoft.AspNetCore.Mvc;
using MovieReview.Models;
using MovieReview.Services;

namespace MovieReview.Controllers{
    public class ReviewController : Controller{
        
        private readonly IReview _review;
        public ReviewController(IReview review){
            _review = review;
        }
        public IActionResult CreateReview(Review request){
            if(Request.Cookies.ContainsKey("Token")){
                _review.CreateReview(request);
            string movieId = request.imdbID;
            return RedirectToAction("Details", "Movie", new {movieId = movieId});
                
            }
            else{
                return Content("You need to log in first");
            }
            
        } 
        public IActionResult DeleteReview(string reviewId){
            var result = _review.DeleteReview(reviewId);
            if(result){
                return Content("Successfullt deleted the review");
            }
            return null;
        }
    }
}