using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain.ValueObjects;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.EntityConfiguration;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.Property(e => e.Price)
            .HasConversion(new MoneyAmount.EfCoreValueConverter());
    }
}