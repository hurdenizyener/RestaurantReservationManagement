using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Net;

namespace Application.Contracts.Infrastructure.Persistence.Repositories;

public interface IGenericRepositoryAsync<TEntity, TEntityId> : IQuery<TEntity>
    where TEntity : BaseEntity<TEntityId>
{
    Task<bool> AnyAsync(
       Expression<Func<TEntity, bool>>? predicate = null,
       bool enableTracking = false,
       CancellationToken cancellationToken = default);

    Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = false,
        CancellationToken cancellationToken = default);

    Task<List<TEntity>> GetAllListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = false,
        CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity);

    Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);

    void Update(TEntity entity);

    void UpdateRange(ICollection<TEntity> entities);

    void Delete(TEntity entity);

    void DeleteRange(ICollection<TEntity> entities);

}

public interface IQuery<T>
{
    IQueryable<T> Query();
}
