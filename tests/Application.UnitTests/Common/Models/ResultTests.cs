namespace Application.UnitTests.Common.Models;

using Application.Common.Models;
using System.Net;
using Xunit;

public class ResultTests
{
    [Fact]
    public void Success_Should_Set_Default_Status_To_OK()
    {
        var result = Result.Success();

        Assert.Equal(HttpStatusCode.OK, result.Status);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public void Success_Should_Set_Custom_Status_Code()
    {
        var customStatus = HttpStatusCode.Created;

        var result = Result.Success(customStatus);

        Assert.Equal(customStatus, result.Status);
        Assert.True(result.IsSuccess);
    }
}
