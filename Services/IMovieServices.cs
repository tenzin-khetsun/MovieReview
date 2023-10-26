using MovieReview.Models;

namespace LoginDb.Interface{
    public interface IMovie{
        public Api SearchMethod(string request);
        public Api MovieDetailsMethod(string request);
    }
}