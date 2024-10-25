using Domain.Common;

namespace Domain.Entities;

public sealed class Table : BaseEntity<Guid>
{
    public int Number { get; set; }
    public int Capacity { get; set; }
    public bool IsAvailable { get; set; }

    public HashSet<Reservation> Reservations { get; set; } = [];

    public Table() : base(Guid.NewGuid()) { }

    public Table(
        Guid id,
        int number,
        int capacity,
        bool isAvailable) : base(id != Guid.Empty ? id : Guid.NewGuid())
    {
        Number = number;
        Capacity = capacity;
        IsAvailable = isAvailable;
    }

}
