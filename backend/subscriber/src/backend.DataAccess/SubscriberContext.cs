using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace backend.DataAccess
{
    public class SubscriberContext : DbContext
    {
        public SubscriberContext(DbContextOptions options) : base(options) { }

        public DbSet<Subscriber> Subscribers { get; set; }

        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
