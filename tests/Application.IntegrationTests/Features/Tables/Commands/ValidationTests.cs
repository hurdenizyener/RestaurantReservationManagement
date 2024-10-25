using Application.Features.Tables.Commands.Create;
using Application.IntegrationTests.Common.Constans;

namespace Application.IntegrationTests.Features.Tables.Commands;

public class TableValidationTests
{
    private readonly CreateTableCommandValidator _validator = new();

    [Theory]
    [InlineData(0, 0)]
    [InlineData(0, 1)]
    [InlineData(0, -1)]
    [InlineData(-1, 1)]
    [InlineData(1, 0)]
    [InlineData(1, -1)]
    public void Validator_Should_Fail_For_Invalid_Values(int number, int capacity)
    {
        var command = new CreateTableCommand(Number: number, Capacity: capacity);

        var result = _validator.Validate(command);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);

        if (number <= 0)
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateTableCommand.Number) &&
                                            (e.ErrorCode == ValidationErrorCodes.NotEmptyValidator ||
                                             e.ErrorCode == ValidationErrorCodes.GreaterThanValidator));

        if (capacity <= 0)
            Assert.Contains(result.Errors, e => e.PropertyName == nameof(CreateTableCommand.Capacity) &&
                                            (e.ErrorCode == ValidationErrorCodes.NotEmptyValidator ||
                                             e.ErrorCode == ValidationErrorCodes.GreaterThanValidator));

    }
}
