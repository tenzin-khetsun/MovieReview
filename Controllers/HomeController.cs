
using Microsoft.AspNetCore.Mvc;
using CinePhile.Database;
using MongoDB.Driver;

namespace MovieReview.Controllers;

public class HomeController : Controller
{
    private readonly IDatabase _movieService;

    public HomeController(IDatabase movieService)
    {
        _movieService = movieService;
    }
    public async Task<IActionResult> Index(int page=1, int pageSize=100)
    {
        int skip = (page-1)*pageSize;
        var movies = await _movieService.Movies().Find(_=>true)
            .Skip(skip)
            .Limit(pageSize)
            .ToListAsync();
        int totalMoviesCount = (int)await _movieService.Movies().CountDocumentsAsync(_ => true);

        int totalPage = (int)Math.Ceiling((double)totalMoviesCount / pageSize);
        ViewBag.TotalPages = totalPage;
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        
       return View(movies);
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
