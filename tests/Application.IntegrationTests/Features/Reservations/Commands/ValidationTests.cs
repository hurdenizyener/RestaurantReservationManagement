using Application.Features.Reservations.Commands.Create;
using Application.IntegrationTests.Common.Constans;
using FluentValidation.TestHelper;

namespace Application.IntegrationTests.Features.Reservations.Commands;

public class ReservationValidationTests
{
    private readonly CreateReservationCommandValidator _validator = new();


    public static class CreateReservationCommandFactory
    {
        public static CreateReservationCommand CreateValidCommand(
            string? customerName = "Test Name",
            string? customerPhone = "+901234567890",
            string? customerEmail = "test@example.com",
            string? specialRequest = "",
            int guestCount = 2,
            DateTimeOffset? reservationDate = null)
        {
            return new CreateReservationCommand
            (
                customerName!,
                customerPhone!,
                customerEmail!,
                specialRequest!,
                guestCount,
                reservationDate ?? DateTimeOffset.Now.AddDays(1)
            );
        }
    }


    [Fact]
    public void Should_HaveError_When_CustomerNameIsEmpty()
    {
        var command = CreateReservationCommandFactory.CreateValidCommand(customerName: "");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.CustomerName).WithErrorCode(ValidationErrorCodes.NotEmptyValidator);

    }

    [Fact]
    public void Should_HaveError_When_CustomerNameIsTooShort()
    {
        var command = CreateReservationCommandFactory.CreateValidCommand(customerName: "A");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.CustomerName).WithErrorCode(ValidationErrorCodes.MinimumLengthValidator);

    }

    [Fact]
    public void Should_HaveError_When_CustomerNameIsTooLong()
    {
        var tooLongName = new string('A', 101);

        var command = CreateReservationCommandFactory.CreateValidCommand(customerName: tooLongName);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.CustomerName).WithErrorCode(ValidationErrorCodes.MaximumLengthValidator);

    }

    [Fact]
    public void Should_HaveError_When_CustomerPhoneIsInvalid()
    {
        var command = CreateReservationCommandFactory.CreateValidCommand(customerPhone: "123456");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.CustomerPhone).WithErrorCode(ValidationErrorCodes.RegularExpressionValidator);
    }


    [Fact]
    public void Should_HaveError_When_CustomerEmailIsInvalid()
    {
        var command = CreateReservationCommandFactory.CreateValidCommand(customerEmail: "email");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.CustomerEmail).WithErrorCode(ValidationErrorCodes.EmailValidator);
    }


    [Fact]
    public void Should_HaveError_When_SpecialRequestIsTooLong()
    {
        var tooLongName = new string('A', 501);

        var command = CreateReservationCommandFactory.CreateValidCommand(specialRequest: tooLongName);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.SpecialRequest).WithErrorCode(ValidationErrorCodes.MaximumLengthValidator);
    }

    [Fact]
    public void Should_HaveError_When_GuestCountIsZeroOrLess()
    {
        var command = CreateReservationCommandFactory.CreateValidCommand(guestCount: 0);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.GuestCount).WithErrorCode(ValidationErrorCodes.GreaterThanValidator);


    }

    [Fact]
    public void Should_HaveError_When_ReservationDateIsInThePast()
    {
        var reservationDate = DateTimeOffset.Now.AddMinutes(-2);

        var command = CreateReservationCommandFactory.CreateValidCommand(reservationDate: reservationDate);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.ReservationDate).WithErrorCode(ValidationErrorCodes.GreaterThanValidator);


    }

    [Fact]
    public void Should_HaveError_When_ReservationDateIsTooFarInTheFuture()
    {
        var reservationDate = DateTimeOffset.Now.AddYears(2);

        var command = CreateReservationCommandFactory.CreateValidCommand(reservationDate: reservationDate);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(r => r.ReservationDate).WithErrorCode(ValidationErrorCodes.LessThanValidator);
    }

}
