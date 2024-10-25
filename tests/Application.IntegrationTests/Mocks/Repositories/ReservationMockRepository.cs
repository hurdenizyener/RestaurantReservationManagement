using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Reservations.Profiles;
using Application.Features.Reservations.Rules;
using Application.IntegrationTests.Mocks.Abstractions;
using Application.IntegrationTests.Mocks.Data;
using Domain.Entities;

namespace Application.IntegrationTests.Mocks.Repositories;

public class ReservationMockRepository() : BaseMockRepository<IReservationRepository, Reservation, Guid, MappingProfiles, ReservationBusinessRules>(ReservationData.GetInitialData()) { }


