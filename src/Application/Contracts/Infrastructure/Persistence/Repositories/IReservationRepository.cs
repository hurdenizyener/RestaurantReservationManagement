using Domain.Entities;

namespace Application.Contracts.Infrastructure.Persistence.Repositories;

public interface IReservationRepository : IGenericRepositoryAsync<Reservation, Guid> { }