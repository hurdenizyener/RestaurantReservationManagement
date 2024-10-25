using Application.Common.Exceptions;
using Application.Features.Tables.Commands.Delete;
using Application.Features.Tables.Constans;
using Application.IntegrationTests.Mocks.Repositories;
using static Application.Features.Tables.Commands.Delete.DeleteTableCommand;

namespace Application.IntegrationTests.Features.Tables.Commands;

public class DeleteTableTests(TableMockRepository repository) : IClassFixture<TableMockRepository>
{
    private readonly DeleteTableCommandHandler _handler = new(
            repository.MockRepository.Object,
            repository.MockUnitOfWork.Object,
            repository.BusinessRules);

    [Fact]
    public async Task Should_Delete_Table()
    {
        Guid tableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7");
        var command = new DeleteTableCommand(tableId);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task Should_ThrowException_When_TableDoesNotExist()
    {
        var tableId = Guid.NewGuid();
        var command = new DeleteTableCommand(tableId);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(command, CancellationToken.None));
        Assert.Equal(TableExceptionMessages.TableDoesNotExist, exception.Message);
    }

}
