using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace backend.DataAccess
{
    public class EmailContext : DbContext
    {
        public EmailContext(DbContextOptions options) : base(options) { }

        public DbSet<Subscriber> Subscribers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
