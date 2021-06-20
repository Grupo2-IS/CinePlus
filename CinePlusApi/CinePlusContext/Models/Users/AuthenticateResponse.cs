using System.Text.Json.Serialization;
using CinePlus.Entities;

namespace CinePlus.Models.Users
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public string Role { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(User user, string jwtToken, string refreshToken)
        {
            this.Id = user.UserID;
            this.FirstName = user.Name;
            this.Username = user.Nick;
            this.JwtToken = jwtToken;
            this.RefreshToken = refreshToken;
            this.Role = user.Role;
        }
    }
}