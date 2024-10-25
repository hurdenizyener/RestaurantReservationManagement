namespace Domain.Interfaces;

internal interface IHasKey<TId>
{
    TId Id { get; set; }
}
