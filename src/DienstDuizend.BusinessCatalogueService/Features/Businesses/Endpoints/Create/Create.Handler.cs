using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain.ValueObjects;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Utilities;
using DienstDuizend.Events;
using MassTransit;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.Create;

[Handler]
public static partial class Create
{
    public record Command(
        string Name,
        string? Description,
        Email? BusinessEmail,
        string KvkNumber,
        Uri? WebsiteUri
    );

    public record Response(
        Guid Id,
        string Name,
        string? Description,
        Email? BusinessEmail,
        string KvkNumber,
        Uri? WebsiteUri
    );

    private static async ValueTask<Response> HandleAsync(
        Command request,
        IBusinessRepository businessRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        IKvkService kvkService,
        ICurrentUserProvider currentUserProvider,
        CancellationToken token)
    {
        if (!await kvkService.IsValidKvkNumber(request.KvkNumber))
            throw Error.NotFound("KvkNumber.Unknown", "The given KVK number is invalid.");

        var business = new Business()
        {
            Name = MarkdownSanitizer.Sanitize(request.Name),
            Description = request.Description is null ? request.Description : MarkdownSanitizer.Sanitize(request.Description),
            KvkNumber = request.KvkNumber,
            WebsiteUri = request.WebsiteUri,
            BusinessEmail = request.BusinessEmail,
            UserId = currentUserProvider.GetCurrentUserId()
        };

        await businessRepository.AddAsync(business, token);
        await unitOfWork.SaveChangesAsync(token);
        
        await publishEndpoint.Publish<BusinessCreatedEvent>(new (
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
            business.BusinessEmail,
            business.KvkNumber,
            business.WebsiteUri
        );
    }
}