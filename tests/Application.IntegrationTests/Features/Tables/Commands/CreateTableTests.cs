using Application.Common.Exceptions;
using Application.Features.Tables.Commands.Create;
using Application.Features.Tables.Constans;
using Application.IntegrationTests.Common.Constans;
using Application.IntegrationTests.Mocks.Repositories;
using Domain.Entities;
using Moq;
using static Application.Features.Tables.Commands.Create.CreateTableCommand;

namespace Application.IntegrationTests.Features.Tables.Commands;
public class CreateTableTests(TableMockRepository repository) : IClassFixture<TableMockRepository>
{
    private readonly CreateTableCommandHandler _handler = new(
            repository.MockRepository.Object,
            repository.MockUnitOfWork.Object,
            repository.Mapper,
            repository.BusinessRules);
  

    [Fact]
    public async Task Should_Create_Table()
    {
        var command = new CreateTableCommand(Number: 5, Capacity: 4);

        var result = await _handler.Handle(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);

        repository.MockRepository.Verify(r => r.AddAsync(It.IsAny<Table>()), Times.Once);
        repository.MockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);

    }

    [Fact]
    public async Task Should_ThrowException_When_TableNumberAlreadyExists()
    {
        var command = new CreateTableCommand(Number: 1, Capacity: 4);

        var exception = await Assert.ThrowsAsync<BusinessException>(() => _handler.Handle(command, CancellationToken.None));

        Assert.Equal(TableExceptionMessages.TableNumberAlreadyExists, exception.Message);
    }


}


