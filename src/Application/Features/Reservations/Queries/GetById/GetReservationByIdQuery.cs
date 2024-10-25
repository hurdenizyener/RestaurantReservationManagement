using Application.Common.Exceptions;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Reservations.Constans;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Queries.GetById;

public sealed record GetReservationByIdQuery(Guid Id) : IRequest<GetReservationByIdResponse>
{
    public sealed class GetReservationByIdQueryHandler(
        IReservationRepository reservationRepository,
        IMapper mapper) : IRequestHandler<GetReservationByIdQuery, GetReservationByIdResponse>
    {
        public async Task<GetReservationByIdResponse> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            Reservation? reservation = await reservationRepository.GetAsync(
                predicate: r => r.Id == request.Id,
                include: r => r.Include(t => t.Table!),
                cancellationToken: cancellationToken) ?? throw new NotFoundException(ReservationExceptionMessages.ReservationDoesNotExist);

            GetReservationByIdResponse response = mapper.Map<GetReservationByIdResponse>(reservation);

            return response;
        }
    }
}