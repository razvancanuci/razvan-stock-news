using backend.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.DataAccess.Configurations;

public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AgeQuestion).IsRequired();
        builder.Property(x => x.OccQuestion).IsRequired();
        builder.HasOne(x => x.Subscriber).WithOne(x => x.Answer)
            .HasForeignKey<Answer>(x => x.SubscriberId);
        builder.Property(x => x.SubscriberId).IsRequired();
    }
}