using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Features.Tables.Rules;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tables.Commands.Delete;

public sealed record DeleteTableCommand(Guid Id) : IRequest<Result>
{
    public sealed class DeleteTableCommandHandler(
        ITableRepository tableRepository,
        IUnitOfWork unitOfWork,
        TableBusinessRules tableBusinessRules) : IRequestHandler<DeleteTableCommand, Result>
    {
        public async Task<Result> Handle(DeleteTableCommand request, CancellationToken cancellationToken)
        {
            Table table = await tableBusinessRules.EnsureTableIdExists(request.Id, cancellationToken);

            tableRepository.Delete(table!);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}