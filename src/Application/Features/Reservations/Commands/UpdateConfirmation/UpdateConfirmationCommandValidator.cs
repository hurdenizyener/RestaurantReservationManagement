using Application.Features.Reservations.Constans;
using FluentValidation;

namespace Application.Features.Reservations.Commands.UpdateConfirmation;

public sealed class UpdateConfirmationCommandValidator : AbstractValidator<UpdateConfirmationCommand>
{
    public UpdateConfirmationCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty()
            .WithMessage(ReservationValidationExceptionMessages.ReservationIdCannotBeEmpty);
    }
}