using Application.Common.Exceptions;
using FluentValidation.Results;

namespace Application.UnitTests.Common.Exceptions;

public class ValidationExceptionTests
{
    [Fact]
    public void ValidationException_Should_Have_Default_Message_And_Empty_Errors()
    {
        var exception = new ValidationException();

        Assert.Equal("One or more validation failures have occurred.", exception.Message);
        Assert.Empty(exception.Errors);
    }

    [Fact]
    public void ValidationException_Should_Initialize_Errors_From_ValidationFailures()
    {
        var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Property1", "Error1"),
            new ValidationFailure("Property1", "Error2"),
            new ValidationFailure("Property2", "Error3")
        };

        var exception = new ValidationException(failures);

        Assert.Equal("One or more validation failures have occurred.", exception.Message);
        Assert.Equal(2, exception.Errors.Count);
        Assert.Contains("Property1", exception.Errors.Keys);
        Assert.Contains("Property2", exception.Errors.Keys);

        Assert.Equal(2, exception.Errors["Property1"].Length);
        Assert.Contains("Error1", exception.Errors["Property1"]);
        Assert.Contains("Error2", exception.Errors["Property1"]);

        Assert.Single(exception.Errors["Property2"]);
        Assert.Contains("Error3", exception.Errors["Property2"]);
    }
}
