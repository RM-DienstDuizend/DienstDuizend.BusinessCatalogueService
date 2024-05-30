using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Repository;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;

public sealed class BusinessRepository(ApplicationDbContext dbContext) : GenericRepository<Business>(dbContext), IBusinessRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
}