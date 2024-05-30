using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.Events;
using Immediate.Handlers.Shared;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.Remove;

[Handler]
public static partial class Remove
{
    public record Command(Guid Id);

    public record Response;
    
    private static async ValueTask<Response> HandleAsync(
        Command request,
        IBusinessRepository businessRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        IPublishEndpoint publishEndpoint,
        CancellationToken token)
    {

        Business business = await businessRepository.GetByIdAsync(request.Id);
        
        if (business.UserId != currentUserProvider.GetCurrentUserId())
            throw Error.Forbidden("User.NoPermission", "You do not have the right permissions to update this business.");
        
        businessRepository.Remove(business);
        await unitOfWork.SaveChangesAsync(token);
        
        await publishEndpoint.Publish<BusinessDeletedEvent>(new (
            business.Id
        ), token);

        
        
        return new Response();
    }
}

