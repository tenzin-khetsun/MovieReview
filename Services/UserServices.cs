using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CinePhile.Database;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MovieReview.Models;
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
                new Claim(ClaimTypes.Name, user.UserName)
            };
            if(user.Role=="Admin"){
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            else{
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
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

        public string? ValidateToken(string token)
        {
        try
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
 
            string role = principal.FindFirst(ClaimTypes.Role)?.Value!;
 
            return role;
        }
        catch (Exception)
        {
            return null;
        }
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
                var token = CreateToken(valid);
                _httpContextAccessor?.HttpContext?.Response.Cookies.Append("Token",token,cookiesOption);
                // var user = _users.Users().Find(u => u.UserEmail == request.UserEmail).FirstOrDefault();
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
            _httpContextAccessor?.HttpContext?.Response.Cookies.Delete("Role");
            return true;
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