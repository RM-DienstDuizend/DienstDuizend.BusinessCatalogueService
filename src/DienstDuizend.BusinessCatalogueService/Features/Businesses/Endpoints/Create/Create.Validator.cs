using FluentValidation;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.Create;

public class CreateValidator : AbstractValidator<Create.Command>
{
    public CreateValidator()
    {
    }       
}
