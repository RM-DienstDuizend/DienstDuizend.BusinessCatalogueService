using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Services.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using Immediate.Handlers.Shared;
using Microsoft.EntityFrameworkCore;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.GetById;

[Handler]
public static partial class GetById
{
    public record Query(Guid Id, Guid BusinessId);

    private static async ValueTask<Service> HandleAsync(
        Query request,
        IBusinessRepository businessRepository,
        IServiceRepository serviceRepository,
        CancellationToken token)
    {
        if (!await businessRepository.ExistsAsync(request.BusinessId, cancellationToken: token))
            throw Error.NotFound<Business>();

        Service service = await serviceRepository.GetByIdAsync(request.Id, cancellationToken: token);

        return service;
    }
}

