namespace Infrastructure.Persistence.Configurations.Identity;

public class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
  public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder) => builder.HasKey(x => x.RoleId);
}