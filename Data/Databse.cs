
using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;
using MovieReview.Models;

namespace CinePhile.Database

{

    public class Database : IDatabase

    {
        // private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Movie> _movies;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Review> _reviews;
        private readonly IMongoCollection<Api> _apiMovies;

        public Database(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["ConnectionStrings:ConnectionStr"]);
            var database = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"]);
            _apiMovies = database.GetCollection<Api>(configuration["ConnectionStrings:ApiMovieCollectionName"]);
            _users = database.GetCollection<User>(configuration["ConnectionStrings:UsersCollectionName"]);
            _reviews = database.GetCollection<Review>(configuration["ConnectionStrings:ReviewCollectionName"]);
            _movies = database.GetCollection<Movie>(configuration["ConnectionStrings:MovieCollectionName"]);
        }
        public IMongoCollection<Movie> Movies()
        {
            return _movies;
        }
        public IMongoCollection<User> Users(){
            return _users;
        }
        public IMongoCollection<Review> Review(){
            return _reviews;
        }
        

        public IMongoCollection<Api> ApiMovies()
        {
            return _apiMovies;
        }
    }
}