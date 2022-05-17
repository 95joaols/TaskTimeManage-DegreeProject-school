using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.Identity;
internal class IdentityUserRoleConfig : IEntityTypeConfiguration<IdentityUserRole<string>>
{
  public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
  {
    builder.HasKey(x => x.RoleId);
  }
}
