using Domain.Entities;

namespace Application.IntegrationTests.Mocks.Data;

internal static class ReservationData
{
  
    internal static List<Reservation> GetInitialData()
    {
        DateTimeOffset date = new DateTimeOffset(2024, 10, 28, 12, 0, 0, TimeSpan.Zero);

        return
        [
            new Reservation {
                Id = Guid.NewGuid(),
                TableId = Guid.NewGuid(),
                CustomerName = "TestName1",
                CustomerPhone="+901111111111",
                CustomerEmail="test@test.com",
                SpecialRequest="",
                GuestCount=2,
                ReservationDate=DateTimeOffset.Now.AddSeconds(15),
                IsConfirmed=false,
            },
                new Reservation {
                Id = Guid.NewGuid(),
                TableId = Guid.NewGuid(),
                CustomerName = "TestName1",
                CustomerPhone="+901111111111",
                CustomerEmail="test2@test.com",
                SpecialRequest="test",
                GuestCount=4,
                ReservationDate=DateTimeOffset.Now.AddHours(1),
                IsConfirmed=false,
            },
                new Reservation {
                Id = Guid.Parse("8f0629bd-d88e-47b4-9860-0a0172dc71f5"),
                TableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7"),
                CustomerName = "TestName1",
                CustomerPhone="+901111111111",
                CustomerEmail="test3@test.com",
                SpecialRequest="",
                GuestCount=6,
                ReservationDate=date,
                IsConfirmed=false,
            },
        ];
    }
}


