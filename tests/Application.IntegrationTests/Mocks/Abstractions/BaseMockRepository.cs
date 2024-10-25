using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using AutoMapper;
using Domain.Common;
using Moq;

namespace Application.IntegrationTests.Mocks.Abstractions;

public abstract class BaseMockRepository<TRepository, TEntity, TId, TMappingProfile, TBusinessRules>
    where TEntity : BaseEntity<TId>
    where TRepository : class, IGenericRepositoryAsync<TEntity, TId>
    where TMappingProfile : Profile, new()
    where TBusinessRules : BaseBusinessRules

{
    public Mock<TRepository> MockRepository { get; private set; }
    public Mock<IUnitOfWork> MockUnitOfWork { get; private set; }
    public IMapper Mapper { get; private set; }
    public TBusinessRules BusinessRules { get; private set; }



    public BaseMockRepository(List<TEntity> initialData)
    {
        MapperConfiguration mapperConfig =
            new(c =>
            {
                c.AddProfile<TMappingProfile>();
            });
        Mapper = mapperConfig.CreateMapper();

        MockRepository = MockRepositoryBuilder.GetRepository<TRepository, TEntity, TId>(initialData);
        MockUnitOfWork = new Mock<IUnitOfWork>();
        MockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
        BusinessRules = (TBusinessRules)Activator.CreateInstance(type: typeof(TBusinessRules), MockRepository.Object);
    }
}
