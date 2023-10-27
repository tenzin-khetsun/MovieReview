using MovieReview.Models;

namespace MovieReview.Services{
    public interface IUser{
        // public User LoginMethod();
        public bool RegisterMethod(User request);
        public bool LoginMethod(User request);
        public bool LogoutMethod ();
    }
}