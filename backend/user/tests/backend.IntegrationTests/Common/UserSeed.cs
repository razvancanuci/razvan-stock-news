using backend.DataAccess;
using backend.DataAccess.Entities;
using backend.DataAccess.Utilities;

namespace backend.IntegrationTests.Common
{
    public class UserSeed
    {
        public static void InitializeDbForTests(UserContext db)
        {
            db.Users.AddRange(GetSeedingUsers());
            db.Admins.AddRange(GetSeedingAdmin());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(UserContext db)
        {
            db.Users.RemoveRange(db.Users);
            InitializeDbForTests(db);
        }

        public static IEnumerable<User> GetSeedingUsers()
        {
            return new[]
            {
                new User{Id = Guid.Parse("6ab34493-6146-4520-81f4-9899871d994d"), Username = "vasivasi", Password = Crypto.Encrypt("Vasi1$2"), Role = "Admin" },
                new User{Id = Guid.Parse("0350d100-c8a4-41e9-aa25-6e7b22fab2c3"), Username = "tikitaka", Password = Crypto.Encrypt("Tiki1^2"), Role = "User"},
                new User{Id = Guid.Parse("318246ea-c33e-42f4-8f36-2b29d1e1981d"), Username = "alexutz", Password = Crypto.Encrypt("Alexutz1$"), Role = "User"  }
            };
        }

        public static IEnumerable<Admin> GetSeedingAdmin()
        {
            return new[]
            {
                new Admin
                {
                    Id = Guid.Parse("6ab34493-6146-4520-81f4-9899871d995e"),
                    UserId = Guid.Parse("6ab34493-6146-4520-81f4-9899871d994d"), Email = "acadea@a.ro"
                }
            };
        }
    }
}
