using DienstDuizend.BusinessCatalogueService.Common.Extensions;
using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Common.Dto;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain.ValueObjects;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using Immediate.Handlers.Shared;
using Microsoft.EntityFrameworkCore;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.GetAllWithPagination;

[Handler]
public static partial class GetAllWithPagination
{
    public record Query(int PageSize = 100, int PageIndex = 1);

    public record Response(
        Guid Id,
        string Name,
        string? Description,
        string KvkNumber,
        Email? BusinessEmail,
        Uri? WebsiteUri
        );

    private static async ValueTask<PaginationResult<Response>> HandleAsync(
        Query request,
        IBusinessRepository businessRepository,
        CancellationToken token)
    {
        var businesses = await businessRepository.Query()
            .Paginate(request.PageIndex, request.PageSize)
            .Select(x => new Response(
                x.Id,
                x.Name,
                x.Description,
                x.KvkNumber,
                x.BusinessEmail,
                x.WebsiteUri
                ))
            .ToListAsync(token);

        return new PaginationResult<Response>
        {
            Data = businesses,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalRecords = await businessRepository.Query().CountAsync(token)
        };

        
    }
}