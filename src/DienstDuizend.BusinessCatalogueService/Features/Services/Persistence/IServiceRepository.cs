using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.Repository;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Persistence;

public interface IServiceRepository : IGenericRepository<Service>
{
}