
using Microsoft.AspNetCore.Mvc;
using CinePhile.Database;
using MongoDB.Driver;
using LoginDb.Interface;

namespace MovieReview.Controllers;

public class HomeController : Controller
{
    private readonly IDatabase _DbContext;
    private readonly IMovie _movies;
    public HomeController(IDatabase DbContext, IMovie movies)
    {
        _DbContext = DbContext;
        _movies = movies;
    }
    public async Task<IActionResult> Index(List<string> selectedGenres, int selectedYear, string query)
    {   
        List<string> genreList = new List<string>{
            "Action",
            "Adventure",
            "Animation",
            "Biography",
            "Comedy",
            "Crime",
            "Documentry",
            "Drama",
            "Family",
            "Fantasy",
            "Film Noir",
            "Game-Show",
            "History",
            "Horror",
            "Music",
            "Musical",
            "Mystery",
            "News",
            "Reality-Show",
            "Romance",
            "Sci-Fi",
            "Sport",
            "Talk-Show",
            "Thriller",
            "War",
            "Western"
        };
        ViewBag.GenreList = genreList;
            if(query!=null){
                var filterMovies = _movies.SearchMethod(query);
                return View(filterMovies);
            }
            if(selectedGenres.Count!=0 && selectedYear>1900){
                var filteredMovies = _movies.FilterMoviesByGenreAndYear(selectedGenres, selectedYear);
                return View(filteredMovies);
                }
            else if(selectedGenres.Count==0 && selectedYear>1900){
                var filteredMovies = _movies.FilterMoviesByYear(selectedYear);
                return View(filteredMovies);
            }
            else if(selectedGenres.Count!=0 && selectedYear<=1900){
                var filteredMovies = _movies.FilterMoviesByGenre(selectedGenres);
                return View(filteredMovies);
            }
            else{
            var movies = await _DbContext.Movies().Find(_=>true).ToListAsync();
            return View(movies);  
            }
    }


    public IActionResult Genre()
    {   
        if(Request.Cookies.ContainsKey("Token"))
        {
            return View();
        }
        return RedirectToAction("Index");
    }

}