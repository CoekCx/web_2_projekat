using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NewAvalon.Abstractions.Clock;
using NewAvalon.Abstractions.Contracts;
using NewAvalon.Abstractions.ServiceLifetimes;
using NewAvalon.Abstractions.Services;
using NewAvalon.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NewAvalon.Infrastructure.Authentication
{
    internal sealed class JwtProvider : IJwtProvider, ITransient
    {
        private readonly ISystemTime _systemTime;
        private readonly JwtOptions _jwtOptions;

        public JwtProvider(ISystemTime systemTime, IOptions<JwtOptions> jwtOptions)
        {
            _systemTime = systemTime;
            _jwtOptions = jwtOptions.Value;
        }

        public string Generate(GenerateTokenRequest user)
        {
            var claims = new Claim[]
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email)
            };

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
                SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                null,
                _systemTime.UtcNow.AddHours(3),
                signingCredentials);

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenValue;
        }

        public (string Email, string FirstName, string LastName) GetUserDetailsFromGoogleJwt(string googleToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(googleToken);

            var email = jwtSecurityToken.Claims.First(claim => claim.Type == "email").Value;
            var name = jwtSecurityToken.Claims.First(claim => claim.Type == "name").Value;

            var nameSplit = name.Split(" ");

            return (email, nameSplit[0], nameSplit[nameSplit.Length - 1]);
        }
    }
}
