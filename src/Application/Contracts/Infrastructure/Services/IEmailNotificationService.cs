using Application.Contracts.Infrastructure.Models.Emails;

namespace Application.Contracts.Infrastructure.Services;

public interface IEmailNotificationService
{
    Task SendReservationEmailAsync(ReservationEmail reservationEmail);
}

