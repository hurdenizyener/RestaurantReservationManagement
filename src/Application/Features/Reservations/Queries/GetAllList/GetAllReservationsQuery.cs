using Application.Common.Exceptions;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Reservations.Constans;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Reservations.Queries.GetAllList;

public sealed record GetAllReservationsQuery : IRequest<List<GetAllReservationsResponse>>
{
    public sealed class GetAllReservationsQueryHandler(
        IReservationRepository reservationRepository,
        IMapper mapper) : IRequestHandler<GetAllReservationsQuery, List<GetAllReservationsResponse>>
    {
        public async Task<List<GetAllReservationsResponse>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
        {
            List<Reservation> reservations = await reservationRepository.GetAllListAsync(
                include: r => r.Include(t => t.Table),
                cancellationToken: cancellationToken);

            if (reservations == null || reservations!.Count == 0)
                throw new NotFoundException(ReservationExceptionMessages.ReservationNotFound);

            List<GetAllReservationsResponse> response = mapper.Map<List<GetAllReservationsResponse>>(reservations);
            return response;
        }
    }
}