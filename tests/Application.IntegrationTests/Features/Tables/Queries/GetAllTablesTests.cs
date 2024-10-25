using Application.Common.Exceptions;
using Application.Features.Tables.Constans;
using Application.Features.Tables.Queries.GetAllList;
using Application.IntegrationTests.Mocks.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;
using static Application.Features.Tables.Queries.GetAllList.GetAllTablesQuery;

namespace Application.IntegrationTests.Features.Tables.Queries;

public class GetAllTablesTests(TableMockRepository repository) : IClassFixture<TableMockRepository>
{
    private readonly GetAllListTableQueryHandler _handler = new(
            repository.MockRepository.Object,
            repository.Mapper);


    [Fact]
    public async Task Should_Rotate_All_Tables()
    {
        var query = new GetAllTablesQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.All(result, table => Assert.True(table.Id != Guid.Empty));
        Assert.Contains(result, table => table.IsAvailable);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_When_TableNotFound()
    {

        repository.MockRepository.Setup(repo => 
        repo.GetAllListAsync(
            It.IsAny<Expression<Func<Table, bool>>>(),
            It.IsAny<Func<IQueryable<Table>, IOrderedQueryable<Table>>>(),
            It.IsAny<Func<IQueryable<Table>, IIncludableQueryable<Table, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var query = new GetAllTablesQuery();

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));

        Assert.Equal(TableExceptionMessages.TableNotFound, exception.Message);
    }

}
