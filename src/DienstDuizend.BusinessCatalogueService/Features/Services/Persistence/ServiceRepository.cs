using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Repository;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Persistence;

public sealed class ServiceRepository(ApplicationDbContext dbContext) : GenericRepository<Service>(dbContext), IServiceRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
}