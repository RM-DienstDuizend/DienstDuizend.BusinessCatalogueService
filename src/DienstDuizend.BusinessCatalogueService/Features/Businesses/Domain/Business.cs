using System.ComponentModel.DataAnnotations.Schema;
using DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain.ValueObjects;
using DienstDuizend.BusinessCatalogueService.Features.Services.Domain;
using DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;

namespace DienstDuizend.BusinessCatalogueService.Features.Businesses.Domain;

[Table("Businesses")]
public class Business : BaseEntity 
{
    // General
    public string Name { get; set; }
    public string? Description { get; set; }
    public string KvkNumber { get; set; }
    
    public Email? BusinessEmail { get; set; }
    public Uri? WebsiteUri { get; set; }
    

    // Images
    public Uri? LogoUri { get; set; }
    public Uri? BannerUri { get; set; }
    
    // Owner
    public Guid UserId { get; set; }
    
    public List<Service> Services { get; set; }
}