using MongoDB.Driver;
using MovieReview.Models;

namespace CinePhile.Database

{

    public interface IDatabase

    {

        // public IMongoCollection<User> Users();

        public IMongoCollection<Api> Movies();
        public IMongoCollection<User> Users();

    }

}