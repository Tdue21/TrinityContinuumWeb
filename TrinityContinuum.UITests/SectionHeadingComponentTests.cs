using TrinityContinuum.WebApp.Components.Components;

namespace TrinityContinuum.UITests;

public class SectionHeadingComponentTests : TestContext
{
    [Fact]
    public void SectionHeading_Initializes_With_Expected_Values_Success_Tests()
    {
        var cut = RenderComponent<SectionHeading>(parameters => parameters
            .AddChildContent("Test Title"));
        cut.MarkupMatches(@"<div class=""heading""><h3>Test Title</h3></div>");
    }
}
