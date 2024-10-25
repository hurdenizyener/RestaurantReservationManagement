using Application.Features.Reservations.Constans;
using FluentValidation;

namespace Application.Features.Reservations.Commands.Create;

public sealed class CreateReservationCommandValidator : AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {

        RuleFor(r => r.CustomerName)
            .NotEmpty()
            .WithMessage(ReservationValidationExceptionMessages.CustomerNameCannotBeEmpty)
            .MinimumLength(2)
            .WithMessage(ReservationValidationExceptionMessages.CustomerNameMinimumLength)
            .MaximumLength(100)
            .WithMessage(ReservationValidationExceptionMessages.CustomerNameMaximumLength);

        RuleFor(r => r.CustomerPhone)
            .NotEmpty()
            .WithMessage(ReservationValidationExceptionMessages.CustomerPhoneCannotBeEmpty)
            .Matches(@"^\+[1-9]{1}[0-9]{3,14}$")
            .WithMessage(ReservationValidationExceptionMessages.CustomerPhoneFormat)
            .MaximumLength(15)
            .WithMessage(ReservationValidationExceptionMessages.CustomerPhoneMaximumLength);

        RuleFor(r => r.CustomerEmail)
            .NotEmpty()
            .WithMessage(ReservationValidationExceptionMessages.CustomerEmailCannotBeEmpty)
            .EmailAddress()
            .WithMessage(ReservationValidationExceptionMessages.CustomerEmailAddressValid)
            .MaximumLength(100)
            .WithMessage(ReservationValidationExceptionMessages.CustomerEmailMaximumLength);

        RuleFor(r => r.SpecialRequest)
            .MaximumLength(500)
            .WithMessage(ReservationValidationExceptionMessages.SpecialRequestMaximumLength);

        RuleFor(r => r.GuestCount)
            .GreaterThan(0)
            .WithMessage(ReservationValidationExceptionMessages.GuestCountGreaterThan);

        RuleFor(r => r.ReservationDate)
            .GreaterThan(DateTime.Now)
            .WithMessage(ReservationValidationExceptionMessages.ReservationDateGreaterThan)
            .LessThan(DateTime.Now.AddYears(1))
            .WithMessage(ReservationValidationExceptionMessages.ReservationDateLessThan);

    }
}