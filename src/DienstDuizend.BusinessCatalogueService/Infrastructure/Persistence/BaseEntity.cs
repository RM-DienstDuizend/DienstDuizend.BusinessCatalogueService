using System.ComponentModel.DataAnnotations;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Persistence;

public abstract class BaseEntity
{
    [Key] 
    public Guid Id { get; private set; } = Guid.NewGuid();
}