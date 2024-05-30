using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Utilities;
using DienstDuizend.Events;
using MassTransit;


namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.Update;

[Handler]
public static partial class Update
{
    public record Command(
        Guid Id,
        string Name,
        string? Description,
        Uri? WebsiteUri
    );
    
    public record Response(
        Guid Id,
        string Name,
        string? Description,
        string KvkNumber,
        Uri? WebsiteUri
    );

    private static async ValueTask<Response> HandleAsync(
        Command request,
        IBusinessRepository businessRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        IPublishEndpoint publishEndpoint,
        CancellationToken token)
    {
        Business? business = await businessRepository.GetByIdAsync(request.Id, cancellationToken: token);
        
        if (business.UserId != currentUserProvider.GetCurrentUserId())
            throw Error.Forbidden("User.NoPermission", "You do not have the right permissions to update this business.");

        business.Name = MarkdownSanitizer.Sanitize(request.Name);
        business.Description = request.Description is null ? request.Description : MarkdownSanitizer.Sanitize(request.Description);
        business.WebsiteUri = request.WebsiteUri;  
        
        businessRepository.Update(business);
        
        await unitOfWork.SaveChangesAsync(token);
        
        await publishEndpoint.Publish<BusinessUpdatedEvent>(new (
            business.Id,
            business.Name,
            business.Description,
            business.KvkNumber,
            business.BusinessEmail?.Value,
            business.WebsiteUri,
            business.LogoUri,
            business.BannerUri,
            business.UserId
        ), token);


        return new Response(
            business.Id,
            business.Name,
            business.Description,
            business.KvkNumber,
            business.WebsiteUri
        );
        
    }
}

