
using CinePhile.Database;
using LoginDb.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using MovieReview.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MovieReview.Services
{
    public class MovieService : IMovie {
        private readonly IDatabase _DBcontext;
        // private readonly IDatabase _reviews;
        public MovieService(IDatabase DBcontext){
            _DBcontext = DBcontext;
        }

        public List<Movie> SearchMethod(string query){
            var filter = Builders<Movie>.Filter.Or(
            Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(query, "i")));
            var result = _DBcontext.Movies().Find(filter).ToList();
            return result;
        }

        public List<Movie> FilterMoviesByGenre(List<string> genres)
        {
            FilterDefinition<Movie> MovieFilter = Builders<Movie>.Filter.Empty;
            //var result = genres.ToString();
            foreach(var item in genres)
            {
                var GenreFilter = Builders<Movie>.Filter.Regex("Genre", new BsonRegularExpression(item, "i"));
                MovieFilter = MovieFilter & GenreFilter;
            }
            if(MovieFilter == Builders<Movie>.Filter.Empty)
            {
                return new List<Movie>();
            }
            else
            {
                return _DBcontext.Movies().Find(MovieFilter).ToList();
            }
        }
        public List<Movie> FilterMoviesByYear(int year){
            var filter = Builders<Movie>.Filter.Eq("Year", year);
            var result = _DBcontext.Movies().Find(filter).ToList();
            return result;
        }
        // public FilterDefinition<Movie> someGenre(List<string> genres)
        // {
        //     FilterDefinition<Movie> MovieFilter = Builders<Movie>.Filter.Empty;;
        //     //var result = genres.ToString();
        //     foreach(var item in genres)
        //     {
        //         var GenreFilter = Builders<Movie>.Filter.Regex("Genre", new BsonRegularExpression(item, "i"));
        //         MovieFilter = MovieFilter & GenreFilter;
        //     }
        //     return MovieFilter;
        // }
  
        
        // public List<Movie> FilterMoviesByRequestAndGenre(string request, List<string> genre)
        // {
        //     var SearchFilter = Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
        //     var GenreFilter = Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(genre, "i"));
        //     var MovieFilter = SearchFilter & GenreFilter;
        //     var result = _DBcontext.Movies().Find(MovieFilter).ToList();
        //     return result;
        // }
        // public List<Movie> FilterMoviesByRequestAndYear(string request, int year)
        // {
        //     var SearchFilter = Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
        //     var YearFilter = Builders<Movie>.Filter.Eq("Year", year);
        //     var MovieFilter = SearchFilter & YearFilter;
        //     var result = _DBcontext.Movies().Find(MovieFilter).ToList();
        //     return result;
        // }
        public List<Movie> FilterMoviesByGenreAndYear(List<string> genres, int year)
        {
            var GenreFilter = someGenre(genres);
            var YearFilter = Builders<Movie>.Filter.Eq("Year", year);
            var MovieFilter = GenreFilter & YearFilter;
            var result = _DBcontext.Movies().Find(MovieFilter).ToList();
            return result;
        }

    public FilterDefinition<Movie> someGenre(List<string> genres)
    {
            FilterDefinition<Movie> MovieFilter = Builders<Movie>.Filter.Empty;;
            foreach(var item in genres)
            {
                var GenreFilter = Builders<Movie>.Filter.Regex("Genre", new BsonRegularExpression(item, "i"));
                MovieFilter = MovieFilter & GenreFilter;
            }
            return MovieFilter;
    }

    // public List<Movie> FilterAllThree(string request, List<string> genre, int Year)
    // {
    //     var SearchFilter = Builders<Movie>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
    //     var GenreFilter = Builders<Movie>.Filter.Regex("Genre", new BsonRegularExpression(genre.ToString(), "i"));
    //     var YearFilter = Builders<Movie>.Filter.Eq("Year", Year);

    //     var MovieFilter = SearchFilter & GenreFilter & YearFilter;
    //     var result = _DBcontext.Movies().Find(MovieFilter).ToList();
    //     return result;
    // }


    public ViewPage MovieDetailsMethod(string movieId){
            var results = _DBcontext.Movies().Find(x => x.imdbID == movieId).FirstOrDefault(); 
            var imdbID = results.imdbID;
            if(results==null){
                Console.WriteLine("result is empty");
            }
            ViewPage viewPage = new ViewPage{
                movies = results,
                reviews = _DBcontext.Review().Find(x=>x.imdbID==imdbID).ToList()
                
            };
            return viewPage;
        }
        

    public bool AddMovieMethod(Movie movierequest){
            if(_DBcontext.Movies().Find(x=>x.imdbID==movierequest.imdbID).FirstOrDefault()==null){
                var apiKey = "ab19245e";
                var client = new RestClient("http://www.omdbapi.com");
                var request = new RestRequest();
                request.AddParameter("apiKey",apiKey);
                request.AddParameter("i", movierequest.imdbID);
                var response = client.Execute(request);
                Api? MovieFromApi = JsonConvert.DeserializeObject<Api>(response.Content);
                movierequest.Poster = MovieFromApi.Poster;
                movierequest.Rated = MovieFromApi.Rated;
                _DBcontext.Movies().InsertOne(movierequest);
                return true;
            }
            else{
                return false;
            }
        }

       
    }
}

