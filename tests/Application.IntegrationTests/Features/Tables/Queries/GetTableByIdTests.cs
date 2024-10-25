using Application.Common.Exceptions;
using Application.Features.Tables.Constans;
using Application.Features.Tables.Queries.GetById;
using Application.IntegrationTests.Mocks.Repositories;
using static Application.Features.Tables.Queries.GetById.GetTableByIdQuery;

namespace Application.IntegrationTests.Features.Tables.Queries;

public class GetTableByIdTests(TableMockRepository repository) : IClassFixture<TableMockRepository>
{
    private readonly GetTableByIdQueryHandler _handler = new(
            repository.MockRepository.Object,
            repository.Mapper);


    [Fact]
    public async Task Should_Return_Table_By_Id()
    {
        Guid tableId = Guid.Parse("3249574d-d054-4c2c-a2af-5931205db8a7");
        var query = new GetTableByIdQuery(tableId);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(query.Id, result.Id);
        Assert.Equal(expected: 4, result.Number);
        Assert.Equal(expected: 8, result.Capacity);
    }

    [Fact]
    public async Task Should_ThrowNotFoundException_When_TableDoesNotExist()
    {
        var tableId = Guid.NewGuid();

        var query = new GetTableByIdQuery(tableId);

        var exception = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));

        Assert.Equal(TableExceptionMessages.TableDoesNotExist, exception.Message);
    }
}