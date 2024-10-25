using Application.Contracts.Infrastructure.Persistence.Repositories;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Linq.Expressions;

namespace Application.IntegrationTests.Mocks;


internal static class MockRepositoryBuilder
{
    public static Mock<TRepository> GetRepository<TRepository, TEntity, TId>(List<TEntity> list)
        where TEntity : BaseEntity<TId>
        where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        var mockRepo = new Mock<TRepository>();

        Build<TRepository, TEntity, TId>(mockRepo, list);

        return mockRepo;
    }

    private static void Build<TRepository, TEntity, TId>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity<TId>
        where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        SetupGetAllListAsync<TRepository, TEntity, TId>(mockRepo, entityList);
        SetupGetAsync<TRepository, TEntity, TId>(mockRepo, entityList);
        SetupAnyAsync<TRepository, TEntity, TId>(mockRepo, entityList);
        SetupAddAsync<TRepository, TEntity, TId>(mockRepo, entityList);
        SetupUpdate<TRepository, TEntity, TId>(mockRepo, entityList);
        SetupDelete<TRepository, TEntity, TId>(mockRepo, entityList);
    }

    private static void SetupGetAllListAsync<TRepository, TEntity, TId>(
        Mock<TRepository> mockRepo,
        List<TEntity> entityList)
        where TEntity : BaseEntity<TId>
        where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        mockRepo.Setup(s =>
        s.GetAllListAsync(
            It.IsAny<Expression<Func<TEntity, bool>>>(),
            It.IsAny<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(),
            It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(
            (
                Expression<Func<TEntity, bool>> predicate,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                bool enableTracking,
                CancellationToken cancellationToken) =>
            {
                IQueryable<TEntity> query = entityList.AsQueryable();

                if (predicate != null)
                    query = query.Where(predicate);

                if (orderBy != null)
                    query = orderBy(query);

                return [.. query];
            });
    }

    private static void SetupGetAsync<TRepository, TEntity, TId>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity<TId>
        where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        mockRepo.Setup(s =>
        s.GetAsync(
            It.IsAny<Expression<Func<TEntity, bool>>>(),
            It.IsAny<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(
            (
                Expression<Func<TEntity, bool>> predicate,
                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include,
                bool enableTracking,
                CancellationToken cancellationToken) =>
            {
                return entityList.FirstOrDefault(predicate.Compile());
            });
    }

    private static void SetupAnyAsync<TRepository, TEntity, TId>(Mock<TRepository> mockRepo, List<TEntity> entityList)
      where TEntity : BaseEntity<TId>
      where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        mockRepo.Setup(s =>
        s.AnyAsync(
            It.IsAny<Expression<Func<TEntity, bool>>>(),
            It.IsAny<bool>(),
            It.IsAny<CancellationToken>()
            ))
            .ReturnsAsync(
            (
                Expression<Func<TEntity, bool>> predicate,
                bool enableTracking,
                CancellationToken cancellationToken) =>
            {
                return entityList.AsQueryable().Any(predicate);
            });
    }

    private static void SetupAddAsync<TRepository, TEntity, TId>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity<TId>
        where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        mockRepo
            .Setup(s => s.AddAsync(It.IsAny<TEntity>()))
            .ReturnsAsync((TEntity entity) =>
            {
                entityList.Add(entity);
                return entity;
            });
    }

    private static void SetupUpdate<TRepository, TEntity, TId>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity<TId>
        where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        mockRepo
            .Setup(s => s.Update(It.IsAny<TEntity>()))
            .Callback((TEntity entity) =>
            {
                var existingEntity = entityList.FirstOrDefault(e => e.Id!.Equals(entity.Id));
                if (existingEntity != null)
                {
                    entityList.Remove(existingEntity);
                    entityList.Add(entity);
                }
            });
    }

    private static void SetupDelete<TRepository, TEntity, TId>(Mock<TRepository> mockRepo, List<TEntity> entityList)
        where TEntity : BaseEntity<TId>
        where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    {
        mockRepo
            .Setup(s => s.Delete(It.IsAny<TEntity>()))
            .Callback((TEntity entity) =>
            {
                entityList.Remove(entity);
            });

    }
}
