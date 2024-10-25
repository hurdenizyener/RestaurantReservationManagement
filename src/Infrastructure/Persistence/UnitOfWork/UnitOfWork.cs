using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork<TContext>(TContext Context) : IUnitOfWork where TContext : DbContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Context.SaveChangesAsync(cancellationToken);

}