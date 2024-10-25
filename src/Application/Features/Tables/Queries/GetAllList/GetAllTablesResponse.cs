namespace Application.Features.Tables.Queries.GetAllList;

public sealed record GetAllTablesResponse(
    Guid Id,
    int Number,
    int Capacity,
    bool IsAvailable,
    DateTimeOffset CreatedDate,
    DateTimeOffset LastModifiedDate);
