using Application.Contracts.Infrastructure.Persistence.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Infrastructure.Persistence.Repositories;

public class GenericRepositoryAsync<TEntity, TEntityId>(DbContext context)
    : IGenericRepositoryAsync<TEntity, TEntityId>
    where TEntity : BaseEntity<TEntityId>
{

    public IQueryable<TEntity> Query() => context.Set<TEntity>();

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await context.AddAsync(entity);
        return entity;
    }

    public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
    {
        await context.AddRangeAsync(entities);
        return entities;
    }

    public void Update(TEntity entity)
    {
        context.Update(entity);
        return;
    }

    public void UpdateRange(ICollection<TEntity> entities)
    {
        context.UpdateRange(entities);
        return;
    }

    public void Delete(TEntity entity)
    {
        context.Remove(entity);
        return;
    }

    public void DeleteRange(ICollection<TEntity> entities)
    {
        context.RemoveRange(entities);
        return;
    }

    public async Task<bool> AnyAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        bool enableTracking = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();

        if (!enableTracking)
            queryable = queryable.AsNoTracking();

        if (predicate != null)
            queryable = queryable.Where(predicate);

        return await queryable.AnyAsync(cancellationToken);
    }

    public async Task<List<TEntity>> GetAllListAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();

        if (!enableTracking)
            queryable = queryable.AsNoTracking();

        if (include != null)
            queryable = include(queryable);

        if (predicate != null)
            queryable = queryable.Where(predicate);

        if (orderBy != null)
            return await orderBy(queryable).ToListAsync(cancellationToken).ConfigureAwait(false);

        return await queryable.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
        bool enableTracking = false,
        CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> queryable = Query();

        if (!enableTracking)
            queryable = queryable.AsNoTracking();

        if (include != null)
            queryable = include(queryable);

        return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }
}
