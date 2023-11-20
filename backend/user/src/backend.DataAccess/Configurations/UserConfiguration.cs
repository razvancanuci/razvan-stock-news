using backend.DataAccess.Entities;
using backend.DataAccess.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Password).IsRequired();
            builder.Property(x => x.Role).IsRequired();
            builder.HasCheckConstraint("CK_Users_Role", "\"Role\" IN ('User','Admin')");
            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasData(new[]{
            new User
            {
                Id = Guid.Parse("e72cab11-bd2e-4a60-87f4-da6f36ca3b11"),
                Username = "vasiletraian",
                Password = Crypto.Encrypt("Miketyson123$"),
                Role = "Admin"
            },
            new User
            {
                 Id = Guid.Parse("631017b9-67a2-4e9f-9942-a45251883a1d"),
                Username = "traianelemer",
                Password = Crypto.Encrypt("America4$"),
                Role = "User"
            } });
        }
    }
}

