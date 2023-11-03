using RestSharp;
using MovieReview.Models;

using Microsoft.AspNetCore.Mvc;
using MovieReview.Services;
using LoginDb.Interface;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace MovieReview.Controllers{
    public class MovieController : Controller{
        private readonly IMovie _movieService; 
        private readonly IConfiguration _configuration;
        private readonly IUser _userService;
        public MovieController(IMovie movieService, IUser userService, IConfiguration configuration){
            _movieService = movieService;
            _userService = userService;
            _configuration = configuration;
        }

       
        public IActionResult Index(){

            return View();
        }

        
        public IActionResult Search(string query){
            
            var movieSearch =  _movieService.SearchMethod(query);
            if(movieSearch!=null){
                return View(movieSearch);
            }
            else{
                return Content("Movie not there in Db");
            }
        }

            

        


        public IActionResult Details(string movieId)
        {
            
            var apiKey = "ab19245e";
            var client = new RestClient("http://www.omdbapi.com");
            var request = new RestRequest();
            request.AddParameter("apiKey",apiKey );
            request.AddParameter("i", movieId);
            var response = client.Execute(request);
            Api MovieFromApi = JsonConvert.DeserializeObject<Api>(response.Content);
            string imdbRating = MovieFromApi.imdbRating;
            List<Rating> publicRating = MovieFromApi.Ratings;
            ViewBag.ImdbRating = imdbRating;
            ViewBag.publicRating = publicRating;
            var req = _movieService.MovieDetailsMethod(movieId);
            return View(req);
        }


        public IActionResult AddMovie(){
            if(Request.Cookies.ContainsKey("Token")){
                string role = _userService.ValidateToken(Request.Cookies["Token"]!);
                if(role != null){
                    if(role == "Admin")
                        return View();
                    return Content("You are not authorized");
                }
                return Content("You are not authorized to do the action");
            }
            return Content("You are not logged in");
        }

        [HttpPost]
        //[Authorize(Policy ="Admin")]
        public IActionResult AddMovie(Movie req){
            if(_movieService.AddMovieMethod(req)){
                return RedirectToAction("Index", "Home");
            }
            else{
                return Content("Movie Aready Exists");
            }
        }

    }
}

