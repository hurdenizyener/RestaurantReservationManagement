using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Tables.Profiles;
using Application.Features.Tables.Rules;
using Application.IntegrationTests.Mocks.Abstractions;
using Application.IntegrationTests.Mocks.Data;
using Domain.Entities;

namespace Application.IntegrationTests.Mocks.Repositories;

public class TableMockRepository() : BaseMockRepository<ITableRepository, Table, Guid, MappingProfiles, TableBusinessRules>(TableData.GetInitialData()) { }


