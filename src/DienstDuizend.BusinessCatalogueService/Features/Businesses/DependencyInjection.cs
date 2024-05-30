using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses;

public static class DependencyInjection
{
    public static void AddBusinessSlice(this IServiceCollection services)
    {
        services.AddScoped<IBusinessRepository, BusinessRepository>();
    }
}