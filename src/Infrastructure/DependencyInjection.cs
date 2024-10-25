using Application.Contracts.Infrastructure.Persistence.Contexts;
using Application.Contracts.Infrastructure.Persistence.Repositories;
using Application.Contracts.Infrastructure.Persistence.UnitOfWork;
using Application.Contracts.Infrastructure.Services;
using Infrastructure.Options;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.UnitOfWork;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(ConnectionStringOption.Key);

        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>()!);
            options.UseNpgsql(connectionString);
        });

        services.Configure<MailOption>(configuration.GetSection(MailOption.Key));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));

        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();

        services.AddScoped<ITableRepository, TableRepository>();

        services.AddScoped<IReservationRepository, ReservationRepository>();

        services.AddTransient<IEmailService, EmailService>();
        services.AddTransient<IEmailNotificationService, EmailNotificationService>();

        return services;
    }
}
