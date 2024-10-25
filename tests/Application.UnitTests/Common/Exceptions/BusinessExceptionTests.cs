using Application.Common.Exceptions;

namespace Application.UnitTests.Common.Exceptions;

public class BusinessExceptionTests
{
    [Fact]
    public void BusinessException_Should_Set_Message_Correctly()
    {

        string expectedMessage = "Test error message";

        var exception = new BusinessException(expectedMessage);

        Assert.Equal(expectedMessage, exception.Message);
    }

    [Fact]
    public void BusinessException_Should_Allow_Null_Message()
    {
        var exception = new BusinessException(null);

        Assert.Equal("Exception of type 'Application.Common.Exceptions.BusinessException' was thrown.", exception.Message);
    }
}
