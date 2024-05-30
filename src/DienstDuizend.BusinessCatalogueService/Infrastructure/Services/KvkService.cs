using System.Net;
using DienstDuizend.BusinessCatalogueService.Common.Interfaces;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Services;

public class KvkService(IKvkTestApi kvkTestApi) : IKvkService
{
    public async Task<bool> IsValidKvkNumber(string input)
    {
        var result = await kvkTestApi.GetBasicProfile(input);

        return (result.StatusCode == HttpStatusCode.OK);
    }
}