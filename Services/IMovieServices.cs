using MovieReview.Models;

namespace LoginDb.Interface{
    public interface IMovie{
        public List<Movie> SearchMethod(string request);
        public ViewPage MovieDetailsMethod(string request);
        public bool AddMovieMethod(Movie request);
    }
}