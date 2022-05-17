using Domain.Aggregates.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.moq.Configurations;

internal class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
{
  public void Configure(EntityTypeBuilder<UserProfile> builder)
  {
    builder.HasKey(x => x.Id);
    builder.HasIndex(x => x.PublicId).IsUnique();
    builder.Property(x => x.PublicId).IsRequired();
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.UpdatedAt).IsRequired();

    builder.Property(x => x.IdentityId).IsRequired();
    builder.Property(x => x.Salt).IsRequired();
    builder.Property(x => x.UserName).IsRequired();
    builder.Property(x => x.HashedPassword).IsRequired();
  }
}