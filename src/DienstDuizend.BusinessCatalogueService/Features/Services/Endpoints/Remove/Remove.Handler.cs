using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Services.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.Events;
using Immediate.Handlers.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.Remove;

[Handler]
public static partial class Remove
{
    public record Command(Guid Id, Guid BusinessId);

    public record Response;
    
    private static async ValueTask<Response> HandleAsync(
        Command request,
        IBusinessRepository businessRepository,
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        ICurrentUserProvider currentUserProvider,
        CancellationToken token)
    {

        Business? business = await businessRepository.GetByIdAsync(request.BusinessId, cancellationToken: token);
        
        if (business.UserId != currentUserProvider.GetCurrentUserId())
            throw Error.Forbidden("User.NoPermission", 
                "You do not have the right permissions to update this business.");

        Service? service = await serviceRepository.GetByIdAsync(request.Id, cancellationToken: token);
        
        serviceRepository.Remove(service);

        await unitOfWork.SaveChangesAsync(token);
        
        await publishEndpoint.Publish<ServiceDeletedEvent>(new (service.Id));
        
        return new Response();
    }
}

