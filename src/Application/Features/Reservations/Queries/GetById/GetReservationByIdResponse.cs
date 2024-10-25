using Application.Features.Reservations.Rules;
using Application.Features.Tables.Queries.GetById;
using Application.Features.Tables.Rules;

namespace Application.Features.Reservations.Queries.GetById;

public sealed record GetReservationByIdResponse(
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
