using Domain.Common;

namespace Domain.Entities;

public sealed class Reservation : BaseEntity<Guid>
{
    public string CustomerName { get; set; } = default!;
    public string CustomerPhone { get; set; } = default!;
    public string CustomerEmail { get; set; } = default!;
    public string SpecialRequest { get; set; } = default!;
    public int GuestCount { get; set; }
    public DateTimeOffset ReservationDate { get; set; }
    public bool IsConfirmed { get; set; }

    public Guid TableId { get; set; }
    public Table Table { get; set; } = default!;

    public Reservation() : base(Guid.NewGuid()) { }

    public Reservation(
        Guid id,
        Guid tableId,
        string customerName,
        string customerPhone,
        string customerEmail,
        string specialRequest,
        int guestCount,
        DateTimeOffset reservationDate,
        bool isConfirmed) : base(id != Guid.Empty ? id : Guid.NewGuid())
    {
        CustomerName = customerName;
        CustomerPhone = customerPhone;
        CustomerEmail = customerEmail;
        SpecialRequest = specialRequest;
        GuestCount = guestCount;
        ReservationDate = reservationDate;
        IsConfirmed = isConfirmed;
        TableId = tableId;
    }
}