using MongoDB.Driver;
using MovieReview.Models;

namespace LoginDb.Interface{
    public interface IMovie{
        public List<Movie> SearchMethod(string request);
        public ViewPage MovieDetailsMethod(string request);
        public bool AddMovieMethod(Movie request);
        public List<Movie> FilterMoviesByGenre(List<string> request);
        public List<Movie> FilterMoviesByGenreAndYear(List<string> genre, int year);
        public List<Movie> FilterMoviesByYear(int year);
        public FilterDefinition<Movie> someGenre(List<string> genres);
        // public List<Movie> FilterbyRequest(string query);
        // // public List<Movie> FilterMoviesByRequestAndGenre(string request, List<string> genre);
        // public List<Movie> FilterMoviesByRequestAndYear(string request, int year);
        // public List<Movie> FilterAllThree(string request, List<string> genre, int year);
        

    }
}