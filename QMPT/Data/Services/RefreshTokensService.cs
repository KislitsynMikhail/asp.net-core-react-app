using QMPT.Data.Models;
using QMPT.Exceptions.RefreshTokens;
using System.Linq;

namespace QMPT.Data.Services
{
    public class RefreshTokensService
    {
        public void Insert(RefreshToken refreshToken)
        {
            var usersService = new UsersService();
            usersService.CheckExistence(refreshToken.UserId);

            CheckNoExistence(refreshToken.Token);

            using var db = new DatabaseContext();
            db.RefreshTokens.Add(refreshToken);
            db.SaveChanges();
        }

        public RefreshToken Get(string token)
        {
            using var db = new DatabaseContext();

            var refreshToken = db.RefreshTokens
                .FirstOrDefault(rt => rt.Token == token);
            if (refreshToken is null)
            {
                throw new RefreshTokenNotFoundException();
            }

            return refreshToken;
        }

        public void Update(RefreshToken refreshToken)
        {
            CheckExistence(refreshToken.Id);

            using var db = new DatabaseContext();
            db.RefreshTokens.Update(refreshToken);
            db.SaveChanges();
        }

        public void Delete(RefreshToken refreshToken)
        {
            CheckExistence(refreshToken.Id);

            using var db = new DatabaseContext();
            db.RefreshTokens.Remove(refreshToken);
            db.SaveChanges();
        }

        public bool IsExists(string token)
        {
            using var db = new DatabaseContext();

            return db.RefreshTokens
                .Any(rt => rt.Token == token);
        }

        public bool IsExists(int refreshTokenId)
        {
            using var db = new DatabaseContext();

            return db.RefreshTokens
                .Any(rt => rt.Id == refreshTokenId);
        }

        public void CheckNoExistence(string token)
        {
            if (IsExists(token))
            {
                throw new RefreshTokenAlreadyExistsException();
            }
        }

        public void CheckExistence(int refreshTokenId)
        {
            if (!IsExists(refreshTokenId))
            {
                throw new RefreshTokenNotFoundException();
            }
        }
    }
}
