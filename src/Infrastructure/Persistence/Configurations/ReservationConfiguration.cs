using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class ReservationConfiguration : BaseConfiguration<Reservation, Guid>
{
    public override void Configure(EntityTypeBuilder<Reservation> builder)
    {
        base.Configure(builder);

        builder
            .ToTable("Reservations");

        builder
            .Property(r => r.CustomerName)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(r => r.CustomerPhone)
            .IsRequired()
            .HasMaxLength(15);

        builder
            .Property(r => r.CustomerEmail)
            .IsRequired()
            .HasMaxLength(100);

        builder
            .Property(r => r.SpecialRequest)
            .HasMaxLength(500);

        builder
            .Property(r => r.GuestCount)
            .HasColumnType("SMALLINT")
            .IsRequired();

        builder
            .Property(r => r.ReservationDate)
            .IsRequired();

        builder
            .Property(r => r.IsConfirmed)
            .IsRequired();

        builder
            .HasOne(r => r.Table)
            .WithMany(t => t.Reservations)
            .HasForeignKey(r => r.TableId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(r => r.CustomerName)
            .HasDatabaseName("idx_customer_name");

        builder
            .HasIndex(r => r.CustomerPhone)
            .HasDatabaseName("idx_customer_phone");

        builder
            .HasIndex(r => r.CustomerEmail)
            .HasDatabaseName("idx_customer_email");

        builder
            .HasIndex(r => r.ReservationDate)
            .HasDatabaseName("idx_reservation_date");
    }
}
