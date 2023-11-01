using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieReview.Models{
    

    public class User {
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId {get; set;}
        [BsonElement("name")]
        public required string UserName { get; set; }
        [BsonElement("email")]
        public required string? UserEmail { get; set; }
        [BsonElement("password")]
        public required string? UserPassword { get; set; }
         [BsonElement("role")]
        public string? Role { get; set; } = "User";
  
    }
    
    
    
}