using Application.Common.Exceptions;
using Application.Features.Tables.Constans;
using Application.Features.Tables.Rules;
using Application.IntegrationTests.Mocks.Repositories;

namespace Application.IntegrationTests.Features.Tables.Rules;

public class TableBusinessRulesTests(TableMockRepository repository) : IClassFixture<TableMockRepository>
{
    private readonly TableBusinessRules businessRules = new(
        repository.MockRepository.Object);

    [Fact]
    public async Task EnsureTableIdExists_Should_ThrowBusinessException_WhenTableDoesNotExist()
    {
        var tableId = Guid.NewGuid();

        var exception = await Assert.ThrowsAsync<BusinessException>(() => repository.BusinessRules.EnsureTableIdExists(tableId, CancellationToken.None));
        Assert.Equal(TableExceptionMessages.TableDoesNotExist, exception.Message);
    }

    [Fact]
    public async Task EnsureTableNumberIsUnique_Should_ThrowBusinessException_WhenTableNumberAlreadyExists()
    {
        int tableNumber = 2;

        var exception = await Assert.ThrowsAsync<BusinessException>(() => repository.BusinessRules.EnsureTableNumberIsUnique(tableNumber, CancellationToken.None));
        Assert.Equal(TableExceptionMessages.TableNumberAlreadyExists, exception.Message);
    }

    [Fact]
    public async Task EnsureTableNumberIsUniqueForUpdate_Should_ThrowBusinessException_WhenTableNumberAlreadyExists()
    {
        Guid tableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7");
        int tableNumber = 2;

        var exception = await Assert.ThrowsAsync<BusinessException>(() => repository.BusinessRules.EnsureTableNumberIsUniqueForUpdate(tableId, tableNumber, CancellationToken.None));
        Assert.Equal(TableExceptionMessages.TableNumberAlreadyExists, exception.Message);
    }
}



