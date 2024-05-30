using Microsoft.AspNetCore.Mvc;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.Remove;

[ApiController, Route("/{BusinessId}/services/{Id}")]
public class RemoveEndpoint(Remove.Handler handler) : ControllerBase
{
    [HttpDelete]
    public async Task<Remove.Response> HandleAsync(
        [FromRoute] Remove.Command request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request, cancellationToken);
}