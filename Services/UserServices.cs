using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Azure;
using CinePhile.Database;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MovieReview.Models;
using MovieReview.Services;
namespace MovieReview.Services{
    public class UserServices : IUser{
        private readonly IDatabase _users;
        private readonly IConfiguration _configuration;
private readonly IHttpContextAccessor _httpContextAccessor;

        public UserServices(IDatabase users, IConfiguration configuration,IHttpContextAccessor httpContextAccessor){
            _users = users;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //Generating token
        private string CreateToken(User user){
            List<Claim> claims = new()
            {
                new Claim(ClaimTypes.Name, user.UserEmail)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        //Login method
        public bool LoginMethod(User request){  
            var cookiesOption = new CookieOptions{
                Expires = DateTime.Now.AddDays(1),
            };
            User valid = _users.Users().Find(x=>x.UserEmail == request.UserEmail).SingleOrDefault();
            if(valid==null){
                return false;
            }
            else if(request.UserPassword==valid.UserPassword){
                var token = CreateToken(request);
                _httpContextAccessor?.HttpContext?.Response.Cookies.Append("Token",token,cookiesOption);
                var user = _users.Users().Find(u => u.UserEmail == request.UserEmail).FirstOrDefault();
                if(user.Role == "Admin")
                    _httpContextAccessor?.HttpContext?.Response.Cookies.Append("Role",user.Role,cookiesOption);
                return true;
            }
            else{
                return false;
            }

        }
        public bool LogoutMethod(){
            var cookieOptions = new CookieOptions{
                Expires = DateTime.Now.AddMinutes(-1),
            };
            _httpContextAccessor?.HttpContext?.Response.Cookies.Delete("Token");
            return false;
        }
        public bool RegisterMethod(User request){
            var emailExists = _users.Users().Find(x=>x.UserEmail == request.UserEmail).SingleOrDefault();
            if(emailExists!=null){
                return false;
            }
            else{
                _users.Users().InsertOne(request);
                return true;
            }
        }
    }
}