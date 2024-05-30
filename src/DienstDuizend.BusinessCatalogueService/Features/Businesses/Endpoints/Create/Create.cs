using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.Create;

[ApiController, Route("/")]
[Authorize]
public class CreateEndpoint(Create.Handler handler) : ControllerBase
{
    [HttpPost]
    public async Task<Create.Response> HandleAsync(
        [FromBody] Create.Command request,
        CancellationToken cancellationToken = new()
    ) => await handler.HandleAsync(request, cancellationToken);
}