using DienstDuizend.BusinessCatalogueService.Common.ValueObjects;
using Ganss.Xss;
using Markdig;
using ReverseMarkdown;

namespace DienstDuizend.BusinessCatalogueService.Infrastructure.Utilities;

public static class MarkdownSanitizer
{
    private static readonly HtmlSanitizer Sanitizer;
    private static readonly Converter Converter;
    
    static MarkdownSanitizer()
    {
        Sanitizer = new HtmlSanitizer();
        Converter = new Converter();
    }

    public static string Sanitize(string rawContent)
    {
        var unsafeHtml = Markdown.ToHtml(rawContent); // convert raw content to markdown
        var safeHtml = Sanitizer.Sanitize(unsafeHtml); // sanitize html
        return Converter.Convert(safeHtml); // Convert *safe* html to markdown
    }
}