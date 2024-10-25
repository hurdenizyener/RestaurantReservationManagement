using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Features.Tables.Rules;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tables.Commands.Create;

public sealed record CreateTableCommand(
   int Number,
   int Capacity) : IRequest<CreatedTableResponse>
{

    public sealed class CreateTableCommandHandler(
        ITableRepository tableRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        TableBusinessRules tableBusinessRules) : IRequestHandler<CreateTableCommand, CreatedTableResponse>
    {
        public async Task<CreatedTableResponse> Handle(CreateTableCommand request, CancellationToken cancellationToken)
        {
            await tableBusinessRules.EnsureTableNumberIsUnique(request.Number, cancellationToken);

            Table table = mapper.Map<Table>(request);
            table.IsAvailable = true;

            await tableRepository.AddAsync(table);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            CreatedTableResponse response = mapper.Map<CreatedTableResponse>(table);
            return response;
        }
    }

}