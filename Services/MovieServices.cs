
using CinePhile.Database;
using LoginDb.Interface;
using MongoDB.Driver;
using MovieReview.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MovieReview.Services
{
    public class MovieService : IMovie {
        private readonly IDatabase _movies;
        public MovieService(IDatabase movies){
            _movies = movies;
        }

        public Api SearchMethod(string query){
            try{
                var apiKey = "ab19245e";
                string title = char.ToUpper(query[0]) + query.Substring(1);

                // var title = query;
                var existingMovie = _movies.Movies().Find(m => m.Title == title).FirstOrDefault();

                if (existingMovie != null)
                {
                // If the movie already exists, return it
                return existingMovie;
                }
                else{
                    var client = new RestClient("http://www.omdbapi.com");
                    var request = new RestRequest();
                    request.AddParameter("apiKey", apiKey);
                    request.AddParameter("t", title);
                    var response = client.Execute(request);
                    if(response.IsSuccessful){
                        var movie = JsonConvert.DeserializeObject<Api>(response.Content);
                        _movies.Movies().InsertOne(movie);
                        return movie;                    
                    }
                    else{
                        return null!;
                    }
                }
                
                }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null!;
            }       
        }
        public Api MovieDetailsMethod(string movieId){
            var results = _movies.Movies().Find(x => x.imdbID == movieId).FirstOrDefault(); 
            if(results==null){
                Console.WriteLine("result is empty");
            }
            else{
                Console.WriteLine(results);
            }
            
            
            // var apiKey = "ab19245e";
            // string title = char.ToUpper(movieName[0]) + movieName.Substring(1);
            // var client = new RestClient("http://www.omdbapi.com");
            // var request = new RestRequest();
            // request.AddParameter("apiKey", apiKey);
            // request.AddParameter("t", title);
            // var response = client.Execute(request);
            // var movie = JsonConvert.DeserializeObject<Api>(response.Content);
            return results;
        }

       
    }
}

