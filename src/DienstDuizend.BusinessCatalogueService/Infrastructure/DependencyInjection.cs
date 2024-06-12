using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Features.Businesses;
using DienstDuizend.BusinessCatalogueService.Features.Services;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions.Handlers;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Repository;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Services;
using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Metrics;
using Refit;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHandlers();
        services.AddBehaviors();

        services.AddExceptionHandler<ApplicationExceptionHandler>();
        services.AddExceptionHandler<FluentValidationExceptionHandler>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            options.UseNpgsql(connectionString);
        });

        services.AddOpenTelemetry()
            .WithMetrics(builder => builder
                // Metrics provider from OpenTelemetry
                .AddRuntimeInstrumentation()
                .AddAspNetCoreInstrumentation()
                // Metrics provides by ASP.NET Core in .NET 8
                .AddMeter("Microsoft.AspNetCore.Hosting")
                .AddMeter("Microsoft.AspNetCore.Server.Kestrel")
                .AddPrometheusExporter()); // We use v1.7 because currently v1.8 has an issue with formatting.


        services.AddMassTransit(busConfigurator =>
        {
            busConfigurator.SetKebabCaseEndpointNameFormatter();
            busConfigurator.AddConsumers(typeof(IAssemblyMarker).Assembly);
            
            busConfigurator.UsingRabbitMq((context, configurator) =>
            {
                configurator.Host(configuration.GetConnectionString("MessageBroker"));
                
                configurator.ConfigureEndpoints(context);
            });
        });
        
        services
            .AddRefitClient<IKvkTestApi>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://developers.kvk.nl/test/api"));

        services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

        services.AddSingleton<IKvkService, KvkService>();
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        
        // Register Slices
        services.AddBusinessSlice();
        services.UseServiceSlice();

        
        return services;
    }
}