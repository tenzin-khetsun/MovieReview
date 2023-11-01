// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.IdentityModel.Tokens;
// using MovieReview.Models;

// namespace MovieReview.Services{
//     public class Validation{
//         private readonly IConfiguration _configuration;

//         public Validation(IConfiguration configuration){
//             _configuration = configuration;
//         }
//         public string CreateToken(User user){
//             List<Claim> claims = new List<Claim>{
//                 new Claim(ClaimTypes.Name, user.UserName)
//             };
//             if(user.Role=="admin"){
//                 claims.Add(new Claim(ClaimTypes.Role, "admin"));
//             }
//             else {
//                 claims.Add(new Claim(ClaimTypes.Role, "user"));
//             }
//             var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings: Token").Value));
//             var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
//             var token = new JwtSecurityToken(
//                 claims: claims,
//                 expires: DateTime.Now.AddDays(1),
//                 signingCredentials: cred
//             );
//             var jwt = new JwtSecurityTokenHandler().WriteToken(token);
//             return jwt;
//         }
//     }
// }