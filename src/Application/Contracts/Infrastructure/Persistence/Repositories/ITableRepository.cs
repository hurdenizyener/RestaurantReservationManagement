using Domain.Entities;

namespace Application.Contracts.Infrastructure.Persistence.Repositories;

public interface ITableRepository : IGenericRepositoryAsync<Table, Guid> { }
