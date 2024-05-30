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
using Microsoft.EntityFrameworkCore;


namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.Update;

[Handler]
public static partial class Update
{
    public record Command(
        Guid Id,
        Guid BusinessId,
        string Title,
        string? Description,
        MoneyAmount Price,
        int EstimatedDurationInMinutes,
        bool IsHomeService,
        bool IsPubliclyVisible
        );

    private static async ValueTask<Service> HandleAsync(
        Command request,
        IBusinessRepository businessRepository,
        IServiceRepository serviceRepository,
        IUnitOfWork unitOfWork,
        IPublishEndpoint publishEndpoint,
        ICurrentUserProvider currentUserProvider,
        CancellationToken token)
    {
        Business business = await businessRepository.GetByIdAsync(request.BusinessId);

        if (business.UserId != currentUserProvider.GetCurrentUserId())
            throw Error.Forbidden(
                "User.NoPermission",
                "You do not have the right permissions to create a service for this business."
            );

        Service? service = await serviceRepository.GetByIdAsync(request.Id);

        service.Title = MarkdownSanitizer.Sanitize(request.Title);
        service.Description = request.Description is null
            ? request.Description
            : MarkdownSanitizer.Sanitize(request.Description);
        service.Price = request.Price;
        service.EstimatedDurationInMinutes = request.EstimatedDurationInMinutes;
        service.IsHomeService = request.IsHomeService;
        service.IsPubliclyVisible = request.IsPubliclyVisible;
        
        serviceRepository.Update(service);

        await unitOfWork.SaveChangesAsync(token);
        
        await publishEndpoint.Publish<ServiceUpdatedEvent>(new (
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

