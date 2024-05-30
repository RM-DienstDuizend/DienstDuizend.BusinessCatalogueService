
namespace DienstDuizend.BusinessCatalogueService.IntegrationTests.Setup;

[CollectionDefinition(nameof(IntegrationTestCollection), DisableParallelization = true)]
public class IntegrationTestCollection : ICollectionFixture<WebAppFactory>
{
}
