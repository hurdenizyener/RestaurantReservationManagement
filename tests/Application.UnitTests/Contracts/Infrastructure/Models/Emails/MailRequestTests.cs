using Application.Contracts.Infrastructure.Models.Emails;

namespace Application.UnitTests.Contracts.Infrastructure.Models.Emails;

public class MailRequestTests
{
    [Fact]
    public void MailRequest_Should_Set_Properties_Correctly()
    {
        var emailTo = "test@test.com";
        var subject = "Test Subject";
        var body = "Test Body";

        var mailRequest = new MailRequest(emailTo, subject, body);

        Assert.Equal(emailTo, mailRequest.EmailTo);
        Assert.Equal(subject, mailRequest.Subject); 
        Assert.Equal(body, mailRequest.Body); 
    }
}
