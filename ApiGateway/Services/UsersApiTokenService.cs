using System;
using ApiGateway.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiGateway.Services
{
    public class UsersApiTokenService
    {
        public AuthToken GenerateToken(AuthUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("$2a$12$kuoCRnGJg7RA.u51BLLIfO/K94.xzMHTt/0GEQ6LKJCpmPiv1sHv6"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expirationDate = DateTime.UtcNow.AddHours(2);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                                            claims: claims,
                                              expires: expirationDate,
                                              signingCredentials: credentials);

            var authToken = new AuthToken();
            authToken.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authToken.ExpirationDate = expirationDate;

            return authToken;
        }
    }
}