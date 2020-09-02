using QMPT.Data.Models;
using QMPT.Exceptions.Users;
using System.Linq;

namespace QMPT.Data.Services
{
    public class UsersService
    {
        public void Insert(User user)
        {
            CheckNoExistence(user.Login);

            using var db = new DatabaseContext();
            db.Users.Add(user);
            db.SaveChanges();
        }

        public bool IsExists(string login)
        {
            using var db = new DatabaseContext();

            return db.Users
                .Any(u => u.Login == login);
        }

        public bool IsExists(int userId)
        {
            using var db = new DatabaseContext();

            return db.Users
                .Any(u => u.Id == userId);
        }

        public User Get(int userId)
        {
            using var db = new DatabaseContext();

            var user = db.Users
                .FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public User Get(string login)
        {
            using var db = new DatabaseContext();

            var user = db.Users
                .FirstOrDefault(u => u.Login == login);
            if (user is null)
            {
                throw new UserNotFoundException();
            }

            return user;
        }

        public void CheckExistence(int userId)
        {
            if (!IsExists(userId))
            {
                throw new UserNotFoundException();
            }
        }

        public void CheckNoExistence(string login)
        {
            if (IsExists(login))
            {
                throw new UserLoginAlreadyExistsException();
            }
        }
    }
}
