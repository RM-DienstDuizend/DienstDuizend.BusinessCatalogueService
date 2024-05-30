using Microsoft.AspNetCore.Authorization;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.Update;

[ApiController, Route("/{Id}")]
[Authorize]
public class UpdateEndpoint(Update.Handler handler) : ControllerBase
{
    [HttpPut]
    public async Task<Update.Response> HandleAsync(
        [FromRoute] Guid Id,
        [FromBody] Update.Command request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request with {Id = Id}, cancellationToken);
}