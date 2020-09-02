using QMPT.Data.Models;
using QMPT.Exceptions.KeyValuePairs;
using System.Linq;

namespace QMPT.Data.Services
{
    public class KeyValuePairsService
    {
        public void Insert(KeyValuePair keyValuePair)
        {
            CheckNoExistence(keyValuePair.Key);

            using var db = new DatabaseContext();
            db.KeyValuePairs.Add(keyValuePair);
            db.SaveChanges();
        }

        public KeyValuePair Get(string key)
        {
            using var db = new DatabaseContext();

            var keyValuePair = db.KeyValuePairs.FirstOrDefault(kvp => kvp.Key == key);
            if (keyValuePair is null)
            {
                throw new KeyValuePairNotFoundException();
            }

            return keyValuePair;
        }

        public void Update(KeyValuePair keyValuePair)
        {
            CheckExistence(keyValuePair.Id);

            using var db = new DatabaseContext();
            db.KeyValuePairs.Update(keyValuePair);
            db.SaveChanges();
        }

        public bool IsExists(string key)
        {
            using var db = new DatabaseContext();

            return db.KeyValuePairs.Any(kvp => kvp.Key == key);
        }

        public bool IsExists(int keyValuePairId)
        {
            using var db = new DatabaseContext();

            return db.KeyValuePairs.Any(kvp => kvp.Id == keyValuePairId);
        }

        public void CheckNoExistence(string key)
        {
            if (IsExists(key))
            {
                throw new KeyValuePairAlreadyExistsException();
            }
        }

        public void CheckExistence(int keyValuePairId)
        {
            if (!IsExists(keyValuePairId))
            {
                throw new KeyValuePairNotFoundException();
            }
        }
    }
}
