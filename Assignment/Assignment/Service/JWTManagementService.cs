using Assignment.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment.Service
{
    public class JWTManagementService: IJWTManagementService
    {
        Dictionary<string, string> UserRecords = new Dictionary<string, string>
        {
            { "Rashid", "test123"},
            {"Madhu", "Madhu123" },
            {"Swalih", "Swalih123" }
        };

        private readonly IConfiguration configuration;

        public JWTManagementService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Tokens Authenticate(Users user)
        {
            if(!UserRecords.Any(userData=> userData.Key == user.Name && userData.Value == user.Password))
            {
                return null;
            }

            var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["JWT:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.Name),
                        
                    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                configuration["JWT:Issuer"],
                configuration["JWT:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return new Tokens { Token = new JwtSecurityTokenHandler().WriteToken(token) };
        }

        public Users GetUsers(string userName)
        {
            if(!UserRecords.Any(userData => userData.Key == userName))
            {
                return null;
            }

            return UserRecords.Where(user => user.Key == userName)
                   .Select(userData => new Users { Name = userData.Key, Password = userData.Value}).FirstOrDefault();
        }

    }
}
