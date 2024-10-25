using Application.Common.Exceptions;

namespace Application.UnitTests.Common.Exceptions;

public class NotFoundExceptionTests
{
    [Fact]
    public void NotFoundException_Should_Have_Null_Message_With_Default_Constructor()
    {
        var exception = new NotFoundException();

        Assert.Equal("Exception of type 'Application.Common.Exceptions.NotFoundException' was thrown.", exception.Message);
    }

    [Fact]
    public void NotFoundException_Should_Set_Message_Correctly()
    {
        string expectedMessage = "Not found";

        var exception = new NotFoundException(expectedMessage);

        Assert.Equal(expectedMessage, exception.Message); 
    }

    [Fact]
    public void NotFoundException_Should_Set_Message_And_InnerException_Correctly()
    {

        string expectedMessage = " Not found";
        var innerException = new Exception("Inner exception");

        var exception = new NotFoundException(expectedMessage, innerException);

        Assert.Equal(expectedMessage, exception.Message); 
        Assert.Equal(innerException, exception.InnerException);
    }
}
