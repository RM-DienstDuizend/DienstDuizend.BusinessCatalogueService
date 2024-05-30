namespace DienstDuizend.BusinessCatalogueService.Common.Interfaces;

public interface ICurrentUserProvider
{
    public Guid GetCurrentUserId();
}