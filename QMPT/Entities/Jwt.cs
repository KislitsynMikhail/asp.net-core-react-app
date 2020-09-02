using Microsoft.IdentityModel.Tokens;
using QMPT.Data.Models;
using QMPT.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QMPT.Entities
{
    public class Jwt
    {
        private const int jwtExpireMinutes = 30;

        public string Token { get; private set; }

        public Jwt(AppSettings appSettings, User user)
        {
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var now = DateTime.UtcNow;
            var claims = GetClaims(user);

            var jwt = new JwtSecurityToken(
                notBefore: now,
                expires: now.AddMinutes(jwtExpireMinutes),
                claims: claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256));

            Token = new JwtSecurityTokenHandler().WriteToken(jwt);
        }


        private static List<Claim> GetClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(AppSettings.UserIdJwtKey, user.Id.ToString())
            };
        }
    }
}
