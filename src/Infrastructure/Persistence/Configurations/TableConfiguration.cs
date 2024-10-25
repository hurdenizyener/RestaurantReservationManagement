using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class TableConfiguration : BaseConfiguration<Table, Guid>
{
    public override void Configure(EntityTypeBuilder<Table> builder)
    {
        base.Configure(builder);

        builder
            .ToTable("Tables");

        builder
            .Property(t => t.Number)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder
            .Property(t => t.Capacity)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder
            .Property(t => t.IsAvailable)
            .IsRequired();

        builder
            .HasIndex(indexExpression: t => t.Number, name: "UK_Tables_Number")
            .IsUnique();
    }
}
