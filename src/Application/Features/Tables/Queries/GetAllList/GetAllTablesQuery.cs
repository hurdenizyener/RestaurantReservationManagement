using Application.Common.Exceptions;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Tables.Constans;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tables.Queries.GetAllList;

public sealed record GetAllTablesQuery : IRequest<List<GetAllTablesResponse>>
{
    public sealed class GetAllListTableQueryHandler(
        ITableRepository tableRepository,
        IMapper mapper) : IRequestHandler<GetAllTablesQuery, List<GetAllTablesResponse>>
    {
        public async Task<List<GetAllTablesResponse>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            List<Table> tables = await tableRepository.GetAllListAsync(cancellationToken: cancellationToken);

            if (tables == null || tables.Count == 0)
                throw new NotFoundException(TableExceptionMessages.TableNotFound);

            List<GetAllTablesResponse> response = mapper.Map<List<GetAllTablesResponse>>(tables);
            return response;
        }
    }
}