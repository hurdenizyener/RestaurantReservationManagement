using Application.Common.Models;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Features.Tables.Service;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Reflection;

namespace Application.IntegrationTests;

public class DependencyInjectionTests
{
    private readonly IServiceCollection _services;

    public DependencyInjectionTests()
    {
        _services = new ServiceCollection();
        var tableRepositoryMock = new Mock<ITableRepository>();
        _services.AddSingleton(tableRepositoryMock.Object);
    }

    [Fact]
    public void AddApplicationService_RegistersAllServices()
    {
        _services.AddApplicationService();

        var serviceProvider = _services.BuildServiceProvider();

        var mapper = serviceProvider.GetService<IMapper>();
        Assert.NotNull(mapper);

        var validators = serviceProvider.GetService<IEnumerable<IValidator>>();
        Assert.NotNull(validators);

        var mediator = serviceProvider.GetService<IMediator>();
        Assert.NotNull(mediator);

        var tableService = serviceProvider.GetService<ITableService>();
        Assert.NotNull(tableService);
    }


    [Fact]
    public void AddSubClassesOfType_RegistersAllSubclasses()
    {
        _services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        var serviceProvider = _services.BuildServiceProvider();

        var subclasses = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(BaseBusinessRules)) && t != typeof(BaseBusinessRules));

        foreach (var subclass in subclasses)
        {
            var service = serviceProvider.GetService(subclass);
            Assert.NotNull(service); 
        }
    }
}