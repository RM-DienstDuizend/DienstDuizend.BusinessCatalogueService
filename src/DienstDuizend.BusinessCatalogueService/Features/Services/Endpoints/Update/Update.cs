using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.Update;

[ApiController, Route("/{BusinessId}/services/{Id}")]
[Authorize]
public class UpdateEndpoint(Update.Handler handler) : ControllerBase
{
    [HttpPut]
    public async Task<Service> HandleAsync(
        [FromRoute] Guid BusinessId,
        [FromRoute] Guid Id,
        [FromBody] Update.Command request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request with {BusinessId = BusinessId, Id = Id}, cancellationToken);
}