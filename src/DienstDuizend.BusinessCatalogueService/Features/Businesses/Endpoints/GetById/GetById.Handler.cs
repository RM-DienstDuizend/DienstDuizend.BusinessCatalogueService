using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain.ValueObjects;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using Immediate.Handlers.Shared;
using Microsoft.EntityFrameworkCore;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.GetById;

[Handler]
public static partial class GetById
{
    public record Query(Guid Id);

    public record Response(
        Guid Id,
        string Name,
        string? Description,
        string KvkNumber,
        Email? BusinessEmail,
        Uri? WebsiteUri
    );

    private static async ValueTask<Response> HandleAsync(
        Query request,
        IBusinessRepository businessRepository,
        CancellationToken token)
    {
        var business = await businessRepository.GetByIdAsync(request.Id, cancellationToken: token);
        
        return new Response(
            business.Id,
            business.Name,
            business.Description,
            business.KvkNumber,
            business.BusinessEmail,
            business.WebsiteUri
        );
    }
}