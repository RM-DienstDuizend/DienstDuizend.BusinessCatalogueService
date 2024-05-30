using FluentValidation;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Endpoints.Update;


public class UpdateValidator : AbstractValidator<Update.Command>
{
    public UpdateValidator()
    {
    }       
}
