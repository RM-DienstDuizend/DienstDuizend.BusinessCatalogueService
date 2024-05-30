using DienstDuizend.BusinessCatalogueService.Common.Extensions;
using DienstDuizend.BusinessCatalogueService.Common.Interfaces;
using DienstDuizend.BusinessCatalogueService.Common.Dto;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Persistence;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Services.Persistence;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Exceptions;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;
using Immediate.Handlers.Shared;
using Microsoft.EntityFrameworkCore;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.GetAllWithPagination;

[Handler]
public static partial class GetAllWithPagination
{
    public record Query(Guid BusinessId, int PageSize = 100, int PageIndex = 1);
    
    private static async ValueTask<PaginationResult<Service>> HandleAsync(
        Query request,
        IServiceRepository serviceRepository,
        ICurrentUserProvider currentUserProvider,
        CancellationToken token)
    {
        
        var services = await serviceRepository.Query()
         //   .Include(s => s.Business)
            .Where(s => s.BusinessId == request.BusinessId)
          //  .Where(s => s.Business.UserId == currentUserProvider.GetCurrentUserId() || s.IsPubliclyVisible == true)
            .Paginate(request.PageIndex, request.PageSize)
            .ToListAsync(token);

        return new PaginationResult<Service>
        {
            Data = services,
            PageIndex = request.PageIndex,
            PageSize = request.PageSize,
            TotalRecords = await serviceRepository.Query().CountAsync(token)
        };

        
    }
}