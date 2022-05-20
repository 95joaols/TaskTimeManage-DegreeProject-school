namespace Infrastructure.Persistence.Configurations;

public class WorkTimeConfig : IEntityTypeConfiguration<WorkTime>
{
  public void Configure(EntityTypeBuilder<WorkTime> builder)
  {
    builder.HasKey(x => x.Id);
    builder.HasIndex(x => x.PublicId).IsUnique();
    builder.Property(x => x.PublicId).IsRequired();
    builder.Property(x => x.PublicId).HasDefaultValueSql("uuid_generate_v4()");
    builder.Property(x => x.CreatedAt).IsRequired();
    builder.Property(x => x.UpdatedAt).IsRequired();

    builder.Property(x => x.Time).IsRequired();

    builder.HasOne(x => x.WorkItem)
      .WithMany(x => x.WorkTimes)
      .IsRequired();
  }
}