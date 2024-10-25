using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Tables.Constans;
using Domain.Entities;

namespace Application.Features.Tables.Rules;

public class TableBusinessRules(ITableRepository tableRepository) : BaseBusinessRules
{
    public async Task<Table> EnsureTableIdExists(Guid id, CancellationToken cancellationToken)
    {
        Table? table = await tableRepository.GetAsync(
            predicate: p => p.Id == id,
            cancellationToken: cancellationToken);

        return table ?? throw new BusinessException(TableExceptionMessages.TableDoesNotExist);
    }

    public async Task EnsureTableNumberIsUnique(int number, CancellationToken cancellationToken)
    {
        bool doesExist = await tableRepository.AnyAsync(
           predicate: p => p.Number == number,
           cancellationToken: cancellationToken);

        if (doesExist)
            throw new BusinessException(TableExceptionMessages.TableNumberAlreadyExists);
    }

    public async Task EnsureTableNumberIsUniqueForUpdate(Guid id, int number, CancellationToken cancellationToken)
    {
        bool doesExists = await tableRepository.AnyAsync(
            predicate: p => p.Id != id && p.Number == number,
            cancellationToken: cancellationToken);

        if (doesExists)
            throw new BusinessException(TableExceptionMessages.TableNumberAlreadyExists);
    }
}
