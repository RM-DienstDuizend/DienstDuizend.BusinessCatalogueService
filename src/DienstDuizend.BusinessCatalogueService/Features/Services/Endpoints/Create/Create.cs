using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.Create;

[ApiController, Route("/{BusinessId}/services")]
[Authorize]
public class CreateEndpoint(Create.Handler handler) : ControllerBase
{
    [HttpPost]
    public async Task<Service> HandleAsync(
        [FromRoute] Guid BusinessId,
        [FromBody] Create.Command request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request with {BusinessId = BusinessId}, cancellationToken);
}