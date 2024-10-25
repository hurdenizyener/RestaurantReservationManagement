using FluentValidation;
using Application.Common.Behaviours;
using MediatR;
using Moq;

namespace Application.UnitTests.Common.Behaviours;

public class ValidationBehaviourTests
{
    private readonly Mock<RequestHandlerDelegate<Unit>> _nextDelegateMock;


    public ValidationBehaviourTests()
    {
        _nextDelegateMock = new Mock<RequestHandlerDelegate<Unit>>();
        _nextDelegateMock.Setup(x => x()).ReturnsAsync(Unit.Value);
    }

    [Fact]
    public async Task Handle_Should_Call_Next_When_No_Validators()
    {

        var validators = Enumerable.Empty<IValidator<TestRequest>>();
        var behaviour = new ValidationBehaviour<TestRequest, Unit>(validators);

        var result = await behaviour.Handle(new TestRequest(), _nextDelegateMock.Object, CancellationToken.None);

        _nextDelegateMock.Verify(x => x(), Times.Once);
        Assert.Equal(Unit.Value, result); 
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Validation_Fails()
    {
        var validatorMock = new Mock<IValidator<TestRequest>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), CancellationToken.None))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult(new[]
            {
                new FluentValidation.Results.ValidationFailure("TestProperty", "Validation error")
            }));

        var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
        var behaviour = new ValidationBehaviour<TestRequest, Unit>(validators);

        await Assert.ThrowsAsync<Application.Common.Exceptions.ValidationException>(async () =>
            await behaviour.Handle(new TestRequest(), _nextDelegateMock.Object, CancellationToken.None));

        _nextDelegateMock.Verify(x => x(), Times.Never); 
    }

    [Fact]
    public async Task Handle_Should_Call_Next_When_Validation_Succeeds()
    {
        var validatorMock = new Mock<IValidator<TestRequest>>();
        validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), CancellationToken.None))
            .ReturnsAsync(new FluentValidation.Results.ValidationResult());

        var validators = new List<IValidator<TestRequest>> { validatorMock.Object };
        var behaviour = new ValidationBehaviour<TestRequest, Unit>(validators);

        var result = await behaviour.Handle(new TestRequest(), _nextDelegateMock.Object, CancellationToken.None);

        _nextDelegateMock.Verify(x => x(), Times.Once); 
        Assert.Equal(Unit.Value, result); 
    }

}


public class TestRequest
{
    public string TestProperty { get; set; } = default!;
}