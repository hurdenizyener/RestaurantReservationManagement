using Application.Contracts.Infrastructure.Models.Emails;

namespace Application.UnitTests.Contracts.Infrastructure.Models.Emails;

public class ReservationEmailTests
{
    [Fact]
    public void ReservationEmail_Should_Set_Properties_Correctly()
    {
        var emailTo = "test@test.com";
        var name = "Test Name";
        var tableNumber = 5;
        var reservationDate = DateTimeOffset.Now;
        var guestCount = 3;

        var reservationEmail = new ReservationEmail(emailTo, name, tableNumber, reservationDate, guestCount);

        Assert.Equal(emailTo, reservationEmail.EmailTo); 
        Assert.Equal(name, reservationEmail.Name);
        Assert.Equal(tableNumber, reservationEmail.TableNumber);
        Assert.Equal(reservationDate, reservationEmail.ReservationDate);
        Assert.Equal(guestCount, reservationEmail.GuestCount);
    }
}