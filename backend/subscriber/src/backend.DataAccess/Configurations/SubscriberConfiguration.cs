using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.DataAccess.Configurations
{
    public class SubscriberConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.SubscriptionDate).IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();
            builder.HasData(new[]
            {
                new Subscriber { Id = Guid.Parse("e4066b77-977f-40af-94e1-a65ef4033061"), Name = "Andrei", Email = "razvan-andrei.canuci@student.tuiasi.ro", PhoneNumber = "0707070707", SubscriptionDate = new DateTime(2022,10,27,10,0,0,DateTimeKind.Utc) },
                new Subscriber { Id = Guid.Parse("fb17d1d3-e1e3-4cfc-924c-61219a1faa57"), Name = "Alexandru", Email = "alex_alexutz@niezz.com", PhoneNumber = "0712345678", SubscriptionDate = new DateTime(2022,10,27,11,0,0,DateTimeKind.Utc) }
            });

        }
    }
}
