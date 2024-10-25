namespace Application.Contracts.Infrastructure.Models.Emails;

public sealed class ReservationEmail(string emailTo, string name, int tableNumber, DateTimeOffset reservationDate, int guestCount)
{
    public string EmailTo { get; } = emailTo;
    public string Name { get; } = name;
    public int TableNumber { get; } = tableNumber;
    public DateTimeOffset ReservationDate { get; } = reservationDate;
    public int GuestCount { get; } = guestCount;
}

