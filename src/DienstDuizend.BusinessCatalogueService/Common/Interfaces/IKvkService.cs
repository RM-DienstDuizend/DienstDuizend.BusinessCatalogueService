namespace DienstDuizend.BusinessCatalogueService.Common.Interfaces;

public interface IKvkService
{
    public Task<bool> IsValidKvkNumber(string input);
}