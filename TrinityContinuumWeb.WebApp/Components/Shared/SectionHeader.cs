using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace TrinityContinuum.WebApp.Components.Shared;

public partial class SectionHeader : ComponentBase
{
    [Parameter] public HeadingType HeadingType { get; set; } = HeadingType.Header;

    [Parameter] public int Level { get; set; } = 3;

    [Parameter] public RenderFragment? ChildContent { get; set; }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        base.BuildRenderTree(builder);
        var level = Math.Min(Math.Max(Level, 1), 6);
        builder.OpenElement(0, "div");
        builder.AddAttribute(1, "class", HeadingType == HeadingType.Header ? "header" : "subheader");
        builder.OpenElement(2, $"h{level}");
        builder.AddContent(3, ChildContent);
        builder.CloseComponent();
        builder.CloseComponent();
    }
}

public enum HeadingType
{
    Header,
    SubHeader
}