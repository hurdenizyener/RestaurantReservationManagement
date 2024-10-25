using Application.Features.Tables.Constans;
using FluentValidation;

namespace Application.Features.Tables.Commands.Update;

public sealed class UpdateTableCommandValidator : AbstractValidator<UpdateTableCommand>
{
    public UpdateTableCommandValidator()
    {
        RuleFor(t => t.Id)
            .NotEmpty()
            .WithMessage(TableValidationExceptionMessages.TableIdCannotBeEmpty);

        RuleFor(t => t.Number)
            .NotEmpty()
            .WithMessage(TableValidationExceptionMessages.NumberCannotBeEmpty)
            .GreaterThan(0)
            .WithMessage(TableValidationExceptionMessages.NumberGreaterThan);

        RuleFor(t => t.Capacity)
            .NotEmpty()
            .WithMessage(TableValidationExceptionMessages.CapacityCannotBeEmpty)
            .GreaterThan(0)
            .WithMessage(TableValidationExceptionMessages.CapacityGreaterThan);
    }
}