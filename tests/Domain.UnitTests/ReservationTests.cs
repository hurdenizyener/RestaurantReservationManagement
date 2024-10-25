using Domain.Entities;

namespace Domain.UnitTests;

public class ReservationTests
{
    [Fact]
    public void Reservation_Constructor_Should_Initialize_Default_Values()
    {
        var reservation = new Reservation();

        Assert.NotEqual(Guid.Empty, reservation.Id);
        Assert.Null(reservation.CustomerName);
        Assert.Null(reservation.CustomerPhone);
        Assert.Null(reservation.CustomerEmail);
        Assert.Null(reservation.SpecialRequest);
        Assert.Equal(0, reservation.GuestCount);
        Assert.Equal(default(DateTimeOffset), reservation.ReservationDate);
        Assert.False(reservation.IsConfirmed);
        Assert.Equal(Guid.Empty, reservation.TableId);
        Assert.Null(reservation.Table);
    }

    [Fact]
    public void Reservation_Should_Create_With_Valid_Values()
    {
        var id = Guid.NewGuid();
        var tableId = Guid.NewGuid();
        var customerName = "Test CustomerName";
        var customerPhone = "+9123456789";
        var customerEmail = "test@example.com";
        var specialRequest = "test";
        var guestCount = 4;
        var reservationDate = DateTimeOffset.Now.AddDays(1);
        var isConfirmed = false;


        var reservation = new Reservation(
            id,
            tableId,
            customerName,
            customerPhone,
            customerEmail,
            specialRequest,
            guestCount,
            reservationDate,
            isConfirmed);


        Assert.Equal(id, reservation.Id);
        Assert.Equal(tableId, reservation.TableId);
        Assert.Equal(customerName, reservation.CustomerName);
        Assert.Equal(customerPhone, reservation.CustomerPhone);
        Assert.Equal(customerEmail, reservation.CustomerEmail);
        Assert.Equal(specialRequest, reservation.SpecialRequest);
        Assert.Equal(guestCount, reservation.GuestCount);
        Assert.Equal(reservationDate, reservation.ReservationDate);
        Assert.False(reservation.IsConfirmed);
    }

    [Fact]
    public void Reservation_Should_Generate_New_Id_If_Empty_Guid_Provided()
    {
        var id = Guid.Empty;
        var tableId = Guid.NewGuid();
        var customerName = "Test CustomerName";
        var customerPhone = "+9123456789";
        var customerEmail = "test@example.com";
        var specialRequest = "test";
        var guestCount = 4;
        var reservationDate = DateTimeOffset.Now.AddDays(3);
        var isConfirmed = true;

        var reservation = new Reservation(
            id,
            tableId,
            customerName,
            customerPhone,
            customerEmail,
            specialRequest,
            guestCount,
            reservationDate,
            isConfirmed);

        Assert.NotEqual(Guid.Empty, reservation.Id);
    }
}