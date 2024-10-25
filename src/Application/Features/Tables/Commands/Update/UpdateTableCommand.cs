using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Features.Tables.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tables.Commands.Update;

public sealed record UpdateTableCommand(
   Guid Id,
   int Number,
   int Capacity,
   bool IsAvailable) : IRequest<Result>;

public sealed class UpdateTableCommandHandler(
    ITableRepository tableRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    TableBusinessRules tableBusinessRules) : IRequestHandler<UpdateTableCommand, Result>
{
    public async Task<Result> Handle(UpdateTableCommand request, CancellationToken cancellationToken)
    {
        Table table = await tableBusinessRules.EnsureTableIdExists(request.Id, cancellationToken);
        await tableBusinessRules.EnsureTableNumberIsUniqueForUpdate(request.Id, request.Number, cancellationToken);

        table = mapper.Map(request, table);

        tableRepository.Update(table);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}

