using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.moq.Configurations.Identity;

internal class IdentityUserLoginConfig : IEntityTypeConfiguration<IdentityUserLogin<string>>
{
  public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder) => builder.HasKey(x => x.UserId);
}