using DienstDuizend.BusinessCatalogueService.Common.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.GetAllWithPagination;

[ApiController, Route("/")]
[ResponseCache(Duration = 60)]
[Authorize]
public class GetAllWithPaginationEndpoint(GetAllWithPagination.Handler handler) : ControllerBase
{
    [HttpGet]
    public async Task<PaginationResult<GetAllWithPagination.Response>> HandleAsync(
        [FromQuery] GetAllWithPagination.Query request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request, cancellationToken);
}