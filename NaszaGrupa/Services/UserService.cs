using Microsoft.Extensions.Options;
using NaszaGrupa.Models;
using NaszaGrupa.Views.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace NaszaGrupa.Services
{
    public class UserService : IdUser
    {

       
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Karolina", LastName = "Sulikowska", Username = "Karola", Password = "123" }
        };

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        private string generateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public User Authenticate(string username, string password)
        {
            var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);

            if (user == null)
                return null;

            user.Token = generateToken(user.Id.ToString());
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
           
            return _users.Select(x => {
                x.Password = null;
                return x;
            });
        }
    }
}
