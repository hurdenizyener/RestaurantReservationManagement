using Application.Features.Tables.Constans;
using FluentValidation;

namespace Application.Features.Tables.Commands.Create;

public sealed class CreateTableCommandValidator : AbstractValidator<CreateTableCommand>
{
    public CreateTableCommandValidator()
    {

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