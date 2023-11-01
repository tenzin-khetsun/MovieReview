using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinePhile.Database;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MovieReview.Models;

namespace MovieReview.Services{
    
    public class ReviewService : IReview{
        private readonly IHttpContextAccessor _httpContextAccessor; 
        private readonly IDatabase _users;
        private readonly IConfiguration _configuration;
        public ReviewService(IDatabase users, IHttpContextAccessor httpContextAccessor, IConfiguration configuration){
            _httpContextAccessor = httpContextAccessor;
            _users = users;
            _configuration = configuration;
        }

        
        public Review CreateReview(Review request){
            try{
                Review review = new(){
                imdbID = request.imdbID,
                ReviewUser = getusername(_httpContextAccessor?.HttpContext?.Request.Cookies["Token"]!),
                Ratings = request.Ratings,
                Content = request.Content,
                CreatedAt = DateTime.Now,

            };
            _users.Review().InsertOne(review);
            return review;
            }
            catch(Exception ex){
                Console.WriteLine("Cannot insert review data");
                return null;
            }
        
        }
        public bool DeleteReview(string reviewId){
            try{
                var filter = Builders<Review>.Filter.Eq(r => r.ReviewId, reviewId);
                var result = _users.Review().DeleteOne(filter);
                return result.DeletedCount > 0;

            }
            catch(Exception ex){
                Console.WriteLine("Error deleting review: " + ex.Message);
                return false;
            }
        }
        private string? getusername(string token)
        {
        
        
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
 
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = key
            };
 
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
 
            string name = principal.FindFirst(ClaimTypes.Name)?.Value!;
 
            return name;
        }

    }
}