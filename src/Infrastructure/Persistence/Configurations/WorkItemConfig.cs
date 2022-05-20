namespace Infrastructure.Persistence.Configurations;

public class WorkItemConfig : IEntityTypeConfiguration<WorkItem>
{
  public void Configure(EntityTypeBuilder<WorkItem> builder)
  {
    builder.HasKey(x => x.Id);
    builder.HasIndex(x => x.PublicId).IsUnique();
    builder.Property(x => x.PublicId).IsRequired();
    builder.Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.UpdatedAt).IsRequired();


    builder.Property(x => x.Name).IsRequired();

    builder.HasOne(x => x.User)
      .WithMany(x => x.WorkItems)
      .IsRequired();
  }
}