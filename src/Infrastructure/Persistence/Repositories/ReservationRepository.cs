using Application.Contracts.Infrastructure.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
namespace Infrastructure.Persistence.Repositories;

public sealed class ReservationRepository(ApplicationDbContext context) : GenericRepositoryAsync<Reservation, Guid>(context), IReservationRepository { }

