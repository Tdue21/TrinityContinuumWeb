using Markdig;
using Microsoft.AspNetCore.Components;

namespace TrinityContinuum.WebApp.Services;

public interface IMarkdownRenderService
{
    MarkupString Render(string markdown);
}

public class MarkdownRenderService : IMarkdownRenderService
{
    public MarkupString Render(string markdown)
    {
        var pipeline = new MarkdownPipelineBuilder()
                            .UseAdvancedExtensions()
                            .Build();   
        var result = Markdown.ToHtml(markdown, pipeline);
        return new MarkupString(result);
    }
}
