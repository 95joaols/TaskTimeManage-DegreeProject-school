namespace Infrastructure.Persistence.Configurations.Identity;

public class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
  public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) => builder.HasNoKey();
}