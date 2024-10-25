using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Contracts.Infrastructure.Persistence.Contexts;

public interface IApplicationDbContext
{
    DbSet<Table> Tables { get; }
    DbSet<Reservation> Reservations { get; }
}