using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.moq.Configurations.Identity;

internal class IdentityUserTokenConfig : IEntityTypeConfiguration<IdentityUserToken<string>>
{
  public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder) => builder.HasNoKey();
}