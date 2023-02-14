using Microsoft.IdentityModel.Tokens;
using NZwalks.APi.Models.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZwalks.APi.Repository
{
    public class TokenHandler : Itokenhandler
    {
        private readonly IConfiguration configuration;

        public TokenHandler(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<string> CreateTokenAsync(User user)
        {
            
            //create a class
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.GivenName, user.Fname));
            claims.Add(new Claim(ClaimTypes.Surname, user.Lname));
            claims.Add(new Claim(ClaimTypes.Email, user.EmailAddress));


            //loop into the roles of users

            user.Roles.ForEach((Role) =>
            {
                claims.Add(new Claim(ClaimTypes.Role, Role));
            });


            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            //token creation

            var token = new JwtSecurityToken(
                configuration["JWT:Issuer"] ,
                configuration["JWT:Audience"],
                claims,
                expires:DateTime.Now.AddMinutes(15),
                signingCredentials:credentials
                );
           return Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));

        }
    }
}
