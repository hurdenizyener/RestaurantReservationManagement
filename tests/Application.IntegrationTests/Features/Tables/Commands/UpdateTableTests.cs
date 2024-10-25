using Application.Common.Exceptions;
using Application.Features.Tables.Commands.Update;
using Application.Features.Tables.Constans;
using Application.IntegrationTests.Mocks.Repositories;
using Domain.Entities;
using Moq;

namespace Application.IntegrationTests.Features.Tables.Commands;

public class UpdateTableTests(TableMockRepository repository) : IClassFixture<TableMockRepository>
{
    private readonly UpdateTableCommandHandler _handler = new(
           repository.MockRepository.Object,
           repository.MockUnitOfWork.Object,
           repository.Mapper,
           repository.BusinessRules);


    [Fact]
    public async Task Should_Update_Table()
    {
        Guid tableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7");
        var command = new UpdateTableCommand(tableId, Number: 5, Capacity: 4, true);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);

        repository.MockRepository.Verify(r => r.Update(It.IsAny<Table>()), Times.Once);
        repository.MockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task Should_ThrowException_When_TableDoesNotExist()
    {
        var tableId = Guid.NewGuid();
        var command = new UpdateTableCommand(tableId, Number: 5, Capacity: 4, true);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal(TableExceptionMessages.TableDoesNotExist, exception.Message);
    }

    [Fact]
    public async Task Should_ThrowException_When_TableNumberAlreadyExists()
    {
        Guid tableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7");
        var command = new UpdateTableCommand(tableId, Number: 2, Capacity: 4, true);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal(TableExceptionMessages.TableNumberAlreadyExists, exception.Message);
    }

}
