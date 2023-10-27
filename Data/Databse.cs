
using System.Diagnostics.CodeAnalysis;
using MongoDB.Driver;
using MovieReview.Models;

namespace CinePhile.Database

{

    public class Database : IDatabase

    {
        // private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Api> _movies;
        private readonly IMongoCollection<User> _users;

        public Database(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["ConnectionStrings:ConnectionStr"]);
            var database = client.GetDatabase(configuration["ConnectionStrings:DatabaseName"]);
            _movies = database.GetCollection<Api>(configuration["ConnectionStrings:MovieCollectionName"]);
            _users = database.GetCollection<User>(configuration["ConnectionStrings:UsersCollectionName"]);
        }
        public IMongoCollection<Api> Movies()
        {
            return _movies;
        }
        public IMongoCollection<User> Users(){
            return _users;
        }
    }
}