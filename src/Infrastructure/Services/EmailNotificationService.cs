using Application.Contracts.Infrastructure.Models.Emails;
using Application.Contracts.Infrastructure.Services;

namespace Infrastructure.Services;

public sealed class EmailNotificationService(IEmailService emailService) : IEmailNotificationService
{
    public async Task SendReservationEmailAsync(ReservationEmail reservationEmail)
    {
        MailRequest mailRequest = new(
            reservationEmail.EmailTo,
            "Rezervasyon Onayı",
            $"Sayın {reservationEmail.Name}," +
            Environment.NewLine +
            Environment.NewLine +
            $"Rezervasyonunuz Başarıyla Alındı." +
            Environment.NewLine +
            Environment.NewLine +
            $"Rezervasyon Tarihi: {reservationEmail.ReservationDate}" +
            Environment.NewLine +
            $"Masa No:{reservationEmail.TableNumber}" +
            Environment.NewLine +
            $"Kişi Sayısı: {reservationEmail.GuestCount}" +
            Environment.NewLine +
            Environment.NewLine +
            "Rezervasyon Onay Link:www.test.com/onay");

        await emailService.SendEmailAsync(mailRequest);
    }
}


