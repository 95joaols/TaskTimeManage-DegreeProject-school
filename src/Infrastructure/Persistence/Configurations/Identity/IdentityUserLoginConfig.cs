namespace Infrastructure.Persistence.Configurations.Identity;

public class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
  public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) => builder.HasKey(x => x.UserId);
}