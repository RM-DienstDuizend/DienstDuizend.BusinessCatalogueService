using Newtonsoft.Json;

namespace DienstDuizend.BusinessCatalogueService.Common.Dto;

public class KvkApiResponse
{
    [JsonProperty("kvkNummer")]
    public string KvkNumber { get; set; }
}