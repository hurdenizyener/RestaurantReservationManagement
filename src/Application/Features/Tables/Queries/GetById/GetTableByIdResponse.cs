namespace Application.Features.Tables.Queries.GetById;

public sealed record GetTableByIdResponse(
    Guid Id,
    int Number,
    int Capacity,
    bool IsAvailable,
    DateTimeOffset CreatedDate,
    DateTimeOffset LastModifiedDate);
