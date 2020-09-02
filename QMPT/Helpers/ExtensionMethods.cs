using QMPT.Exceptions.AccessTokens;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;

namespace QMPT.Helpers
{
    public static class ExtensionMethods
    {
        public static int ToInt(this HttpStatusCode statusCode)
        {
            return Convert.ToInt32(statusCode);
        }

        public static int ToInt(this string strNum)
        {
            return Convert.ToInt32(strNum);
        }

        public static int GetId(this ClaimsPrincipal user)
        {
            if (user.Claims.Count() == 0)
            {
                throw new AccessTokenNotFoundException();
            }

            return user
                    .Claims
                    .First(c => c.Type == AppSettings.UserIdJwtKey)
                    .Value
                    .ToInt();
        }
    }
}
