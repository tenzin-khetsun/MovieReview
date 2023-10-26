using RestSharp;
using MovieReview.Models;

using Microsoft.AspNetCore.Mvc;
using MovieReview.Services;
using LoginDb.Interface;

namespace MovieReview.Controllers{
    public class MovieController : Controller{
        private readonly IMovie _movieService;
        public MovieController(IMovie movieService){
            _movieService = movieService;
        }
       
        public IActionResult Index(){
            return View();
        }
        public IActionResult Search(string query){
            var api =  _movieService.SearchMethod(query);

            return View(api);
        }
        public IActionResult Details(string movieId){
            var req = _movieService.MovieDetailsMethod(movieId);
            Console.WriteLine(req);
            return View(req);
        }
    }
}

