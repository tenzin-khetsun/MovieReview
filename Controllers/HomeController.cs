using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using MovieReview.Models;
using RestSharp;
using Newtonsoft.Json;
using CinePhile.Database;
using MongoDB.Driver;
using Azure;

namespace MovieReview.Controllers;

public class HomeController : Controller
{
    private readonly IDatabase _movieService;

    public HomeController(IDatabase movieService)
    {
        _movieService = movieService;
    }
    public async Task<IActionResult> Index(int page=1, int pageSize=10)
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
        return View();
    }

}
