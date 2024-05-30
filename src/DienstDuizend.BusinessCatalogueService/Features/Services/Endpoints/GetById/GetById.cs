using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.GetById;

[ApiController, Route("/{BusinessId}/services/{Id}")]
[Authorize]
public class GetByIdEndpoint(GetById.Handler handler) : ControllerBase
{
    [HttpGet]
    public async Task<Service> HandleAsync(
        [FromRoute] GetById.Query request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request, cancellationToken);
}