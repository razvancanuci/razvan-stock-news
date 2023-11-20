using System.Collections;
using backend.DataAccess;
using backend.DataAccess.Entities;

namespace backend.IntegrationTests.Common
{
    public class SubscriberSeed
    {
        public static void InitializeDbForTests(SubscriberContext db)
        {
            db.Subscribers.AddRange(GetSeedingUsers());
            db.Answers.AddRange(GetSeedingAnswers());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(SubscriberContext db)
        {
            db.Subscribers.RemoveRange(db.Subscribers);
            InitializeDbForTests(db);
        }

        public static IEnumerable<Subscriber> GetSeedingUsers()
        {
            return new[]
            {
                new Subscriber{Id = Guid.Parse("6ab34493-6146-4520-81f4-9899871d994d"), Name = "vasivasi", Email = "vasivasi@g.ro", PhoneNumber = "0712345678", SubscriptionDate = new DateTime(2022, 11, 1, 10, 0, 0, DateTimeKind.Utc) },
                new Subscriber{Id = Guid.Parse("0350d100-c8a4-41e9-aa25-6e7b22fab2c3"), Name = "tikitaka", Email = "tikitaka@gmail.com", PhoneNumber = "0707070707", SubscriptionDate = new DateTime(2022, 11, 1, 11, 0, 0, DateTimeKind.Utc) },
                new Subscriber{Id = Guid.Parse("318246ea-c33e-42f4-8f36-2b29d1e1981d"), Name = "alexutz", Email = "alexutz@yahoo.com", SubscriptionDate = new DateTime(2022, 11, 1, 9, 30, 0, DateTimeKind.Utc) }
            };
        }

        public static IEnumerable<Answer> GetSeedingAnswers()
        {
            return new[]
            {
                new Answer
                {
                    Id = Guid.Parse("62b34393-6146-4520-81f4-9899871d994d"), OccQuestion = "Employee", AgeQuestion = 12,
                    SubscriberId = Guid.Parse("318246ea-c33e-42f4-8f36-2b29d1e1981d")
                }
            };
        }
    }
}
