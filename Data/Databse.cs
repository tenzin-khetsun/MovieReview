
using MongoDB.Driver;
using MovieReview.Models;

namespace CinePhile.Database

{

    public class Database : IDatabase

    {
        // private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Api> _movies;

        public Database(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["ConnectionStrings:ConnectionStr"]);
            var database = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"]);
            // _users = database.GetCollection<User>(configuration["Database:UsersCollectionName"]);
            _movies = database.GetCollection<Api>(configuration["ConnectionStrings:MovieCollectionName"]);
        }
        public IMongoCollection<Api> Movies()
        {
            return _movies;
        }
    }
}