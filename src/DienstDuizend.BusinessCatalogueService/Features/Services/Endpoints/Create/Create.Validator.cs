using FluentValidation;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Endpoints.Create;

public class CreateValidator : AbstractValidator<Create.Command>
{
    public CreateValidator()
    {
    }       
}
