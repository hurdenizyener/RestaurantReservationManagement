using Application.Contracts.Infrastructure.Persistence.Repositories;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;
using System.Net;
namespace Infrastructure.Persistence.Repositories;

public sealed class TableRepository(ApplicationDbContext context) : GenericRepositoryAsync<Table, Guid>(context), ITableRepository { }

