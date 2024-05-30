using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence.EntityConfiguration;

public class BusinessConfiguration : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.Property(e => e.BusinessEmail)
            .HasConversion(new Email.EfCoreValueConverter());
    }
}