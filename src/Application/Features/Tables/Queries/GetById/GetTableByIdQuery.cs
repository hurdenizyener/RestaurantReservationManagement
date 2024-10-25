using Application.Common.Exceptions;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Tables.Constans;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Tables.Queries.GetById;

public sealed record GetTableByIdQuery(Guid Id) : IRequest<GetTableByIdResponse>
{
    public sealed class GetTableByIdQueryHandler(
        ITableRepository tableRepository,
        IMapper mapper) : IRequestHandler<GetTableByIdQuery, GetTableByIdResponse>
    {
        public async Task<GetTableByIdResponse> Handle(GetTableByIdQuery request, CancellationToken cancellationToken)
        {
            Table? table = await tableRepository.GetAsync(
                predicate: t => t.Id == request.Id,
                cancellationToken: cancellationToken) ?? throw new NotFoundException(TableExceptionMessages.TableDoesNotExist);

            GetTableByIdResponse response = mapper.Map<GetTableByIdResponse>(table);
            return response;
        }
    }
}