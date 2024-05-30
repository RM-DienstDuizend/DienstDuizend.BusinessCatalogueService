using DienstDuizend.BusinessCatalogueService.Common.Dto;
using Refit;

namespace DienstDuizend.BusinessCatalogueService.Common.Interfaces;

public interface IKvkTestApi
{
    [Get("/v1/basisprofielen/{kvkNumber}")]
    Task<ApiResponse<KvkApiResponse>> GetBasicProfile(string kvkNumber);
}