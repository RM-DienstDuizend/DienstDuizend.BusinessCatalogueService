using DienstDuizend.BusinessCatalogueService.Common.Dto;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.GetAllWithPagination;

[ApiController, Route("/{BusinessId}/services")]
[Authorize]
public class GetAllWithPaginationEndpoint(GetAllWithPagination.Handler handler) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResult<Service>> HandleAsync(
        [FromRoute] Guid BusinessId,
        [FromQuery] GetAllWithPagination.Query request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request with {BusinessId = BusinessId}, cancellationToken);
}