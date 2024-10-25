using Domain.Entities;

namespace Application.Features.Tables.Service;

public interface ITableService
{
    Task<List<Table>> FindAvailableTablesForReservation(DateTimeOffset reservationDate, int guestCount, CancellationToken cancellationToken);
    Task<bool> EnsureTableIdExists(Guid id, CancellationToken cancellationToken);
}
