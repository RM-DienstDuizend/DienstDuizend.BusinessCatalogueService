using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Features.Services.Persistence;

namespace DienstDuizend.BusinessCatalogueService.Features.Services;

public static class DependencyInjection
{
    public static void UseServiceSlice(this IServiceCollection services)
    {
        services.AddScoped<IServiceRepository, ServiceRepository>();
    }
}