using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain.ValueObjects;
using DienstDuizend.BusinessCatalogueService.Features.Services.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Utilities;
using DienstDuizend.Events;
using Immediate.Handlers.Shared;
using MassTransit;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.Create;

[Handler]
public static partial class Create
{
    public record Command(
        string Title,
        string? Description,
        Guid BusinessId,
        MoneyAmount Price,
        int EstimatedDurationInMinutes,
        bool IsPubliclyVisible,
        bool IsHomeService = true

    );

    private static async ValueTask<Service> HandleAsync(
        Command request,
        IBusinessRepository businessRepository,
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        IPublishEndpoint publishEndpoint,
        CancellationToken token)
    {
        Business? business = await businessRepository.GetByIdAsync(request.BusinessId, cancellationToken: token);

        if (business.UserId != currentUserProvider.GetCurrentUserId())
            throw Error.Forbidden(
                "User.NoPermission",
                "You do not have the right permissions to create a service for this business."
            );
        
        var service = new Service()
        {
            Title = MarkdownSanitizer.Sanitize(request.Title),
            Description = request.Description is null ? request.Description : MarkdownSanitizer.Sanitize(request.Description),
            BusinessId = business.Id,
            Price = request.Price,
            EstimatedDurationInMinutes = request.EstimatedDurationInMinutes,
            IsHomeService = request.IsHomeService,
            IsPubliclyVisible = request.IsPubliclyVisible
        };

        await serviceRepository.AddAsync(service, token);
        await unitOfWork.SaveChangesAsync(token);

        await publishEndpoint.Publish<ServiceCreatedEvent>(new (
            service.Id,
            service.Title,
            service.Description,
            service.BusinessId,
            service.Price.Value,
            service.EstimatedDurationInMinutes,
            service.IsHomeService,
            service.IsPubliclyVisible
            ));

        return service;
    }
}