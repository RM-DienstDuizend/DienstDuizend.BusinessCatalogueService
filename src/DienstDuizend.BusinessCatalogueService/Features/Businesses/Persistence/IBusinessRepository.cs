using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Repository;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;

public interface IBusinessRepository : IGenericRepository<Business>
{
}