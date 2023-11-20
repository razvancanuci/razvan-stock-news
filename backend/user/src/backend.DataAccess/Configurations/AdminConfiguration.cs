using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.DataAccess.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.HasOne(x => x.User).WithOne(x => x.Admin)
            .HasForeignKey<Admin>(x => x.UserId);
        builder.HasIndex(x => x.UserId).IsUnique();
        builder.Property(x => x.Email).IsRequired();
        builder.Property(x => x.TwoFactorCode).HasMaxLength(6);

        builder.HasData(
            new Admin
            {
                Id = Guid.Parse("72883c2c-028a-4d09-ae08-db208395f19e"),
                UserId = Guid.Parse("e72cab11-bd2e-4a60-87f4-da6f36ca3b11"),
                Email = "razvan-andrei.canuci@student.tuiasi.ro"
            }
        );
    }
}