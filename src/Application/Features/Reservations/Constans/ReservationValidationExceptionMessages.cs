namespace Application.Features.Reservations.Constans;

internal static class ReservationValidationExceptionMessages
{
    internal const string ReservationIdCannotBeEmpty = "Rezervasyon Id'si Boş Olamaz.";
    internal const string TableIdCannotBeEmpty = "Masa Id'si Boş Olamaz.";
    internal const string IsConfirmedCannotBeEmpty = "Onay Durumu Boş Olamaz.";
    internal const string CustomerNameCannotBeEmpty = "Müşteri Adı Bos Olamaz.";
    internal const string CustomerNameMinimumLength = "Müşteri Adi Minimum 2 Karakter Olmalidir.";
    internal const string CustomerNameMaximumLength = "Müşteri Adi Maksimum 100 Karakter Olmalidir.";
    internal const string CustomerPhoneCannotBeEmpty = "Telefon Numarası Bos Olamaz.";
    internal const string CustomerPhoneMaximumLength = "Telefon Numarası Maksimum 15 Karakter Olmalidir.";
    internal const string CustomerPhoneFormat = "Geçerli Bir Telefon Numarası Giriniz.";
    internal const string CustomerEmailCannotBeEmpty = "E-Posta Adresi Bos Olamaz.";
    internal const string CustomerEmailAddressValid = "Gecerli Bir E-Posta Adresi Giriniz.";
    internal const string CustomerEmailMaximumLength = "E-Posta Adresi Maksimum 100 Karakter Olmalidir.";
    internal const string SpecialRequestMaximumLength = "Özel İstek Maksimum 500 Karakter Olmalidir.";
    internal const string GuestCountGreaterThan = "Konuk Sayısı En Az 1 Kişi Olmalıdır.";
    internal const string ReservationDateGreaterThan = "Rezervasyon Tarihi Bugünden İtibaren Olmalıdır.";
    internal const string ReservationDateLessThan = "Rezervasyon Tarihi 1 Yıldan Uzak Bir Tarih Olamaz.";
}
