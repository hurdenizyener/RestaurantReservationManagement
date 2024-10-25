using Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations;

public class BaseConfiguration<TEntity, TId> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity<TId>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .Property(p => p.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder
            .Property(p => p.CreatedDate)
            .HasColumnName("CreatedDate")
            .IsRequired();

        builder
            .Property(p => p.LastModifiedDate)
            .HasColumnName("LastModifiedDate");

    }
}
