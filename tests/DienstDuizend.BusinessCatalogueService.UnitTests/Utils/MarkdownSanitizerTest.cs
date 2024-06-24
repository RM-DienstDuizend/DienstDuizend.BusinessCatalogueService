using DienstDuizend.BusinessCatalogueService.Infrastructure.Utilities;

namespace DienstDuizend.BusinessCatalogueService.UnitTests.Utils;

public class MarkdownSanitizerTest
{
    [Theory]
    [InlineData("[hello](javascript:prompt(document.cookie))", "hello")]
    [InlineData("<script>alert(1);</script>", "")]
    [InlineData("<img src/onerror=prompt(8)>", "&lt;img src/onerror=prompt(8)&gt;")]
    public void Should_Clean_MaliciousContents(string raw, string after)
    {
        var result = MarkdownSanitizer.Sanitize(raw);
        result.Should().BeEquivalentTo(after);
    }
}