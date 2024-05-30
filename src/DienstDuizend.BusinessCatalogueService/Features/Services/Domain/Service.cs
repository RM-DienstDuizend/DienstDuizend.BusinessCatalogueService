using System.ComponentModel.DataAnnotations.Schema;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain.ValueObjects;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;

namespace DienstDuizend.BusinessCatalogueService.Features.Services.Domain;

[Table("Services")]
public class Service : BaseEntity
{
     public string Title { get; set; }
     public string? Description { get; set; }
     public Guid BusinessId { get; set; }
     public Business Business { get; set; }
     public MoneyAmount Price { get; set; }
     
     public int EstimatedDurationInMinutes { get; set; }
     
     public bool IsHomeService { get; set; }
     public bool IsPubliclyVisible { get; set; }
}