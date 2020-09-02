using QMPT.Data.Models;
using QMPT.Data.Services;
using QMPT.Entities;
using QMPT.Helpers;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QMPT.Models.Responses
{
    public class Tokens
    {
        private readonly Jwt accessToken;
        private readonly RefreshToken refreshToken;

        public string AccessToken { get { return $"Bearer {accessToken.Token}"; } }
        public string RefreshToken => refreshToken.Token;

        public Tokens(Jwt jwt, RefreshToken refreshToken)
        {
            accessToken = jwt;
            this.refreshToken = refreshToken;
        }
    }
}
