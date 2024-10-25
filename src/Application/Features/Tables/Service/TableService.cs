using Application.Contracts.Infrastructure.Persistence.Repositories;
using Domain.Entities;

namespace Application.Features.Tables.Service;

public class TableService(ITableRepository tableRepository) : ITableService
{
    public async Task<bool> EnsureTableIdExists(Guid id, CancellationToken cancellationToken)
    {
        bool doesExists = await tableRepository.AnyAsync(
            predicate: p => p.Id == id,
            cancellationToken: cancellationToken);

        return doesExists;
    }

    public async Task<List<Table>> FindAvailableTablesForReservation(DateTimeOffset reservationDate, int guestCount, CancellationToken cancellationToken)
    {
        List<Table> tables = await tableRepository.GetAllListAsync(
             predicate: t => t.Capacity >= guestCount && t.IsAvailable && !t.Reservations.Any(r => r.ReservationDate == reservationDate),
             orderBy: t => t.OrderBy(t => t.Capacity),
             cancellationToken: cancellationToken);

        return tables;
    }
}