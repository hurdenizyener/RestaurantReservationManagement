namespace Application.Features.Reservations.Queries.GetAllList;

public sealed record GetAllReservationsResponse(
    Guid Id,
    string CustomerName,
    string CustomerPhone,
    string CustomerEmail,
    string SpecialRequest,
    int GuestCount,
    DateTimeOffset ReservationDate,
    bool IsConfirmed,
    int TableNumber,
    DateTimeOffset CreatedDate,
    DateTimeOffset LastModifiedDate);
