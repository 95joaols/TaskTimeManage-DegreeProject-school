﻿namespace Infrastructure.Persistence.Configurations;

public class UserProfileConfig : IEntityTypeConfiguration<UserProfile>
{
  public void Configure(EntityTypeBuilder<UserProfile> builder)
  {
    builder.HasKey(x => x.Id);
    builder.HasIndex(x => x.PublicId).IsUnique();
    builder.Property(x => x.PublicId).IsRequired();
    builder.Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.UpdatedAt).IsRequired();

    builder.Property(x => x.IdentityId).IsRequired();
    builder.Property(x => x.UserName).IsRequired();
  }
}