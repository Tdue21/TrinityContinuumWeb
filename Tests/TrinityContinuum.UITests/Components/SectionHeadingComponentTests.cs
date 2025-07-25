using TrinityContinuum.WebApp.Components.Shared;

namespace TrinityContinuum.UITests.Components;

public class SectionHeadingComponentTests : TestContext
{
    [Fact]
    public void SectionHeading_Default_Initializes_With_Expected_Values_Success_Tests()
    {
        var cut = RenderComponent<SectionHeader>(parameters => 
            parameters.AddChildContent("Test Title"));
        cut.MarkupMatches(@"<div class=""header""><h3>Test Title</h3></div>");
    }

    [Fact]
    public void SectionHeading_Initializes_With_Parameters_Header_Success_Tests()
    {
        var cut = RenderComponent<SectionHeader>(parameters =>
            parameters.Add(x => x.Level, 1)
                      .Add(p => p.HeadingType, HeadingType.Header)
                      .AddChildContent("Test Title"));
        cut.MarkupMatches(@"<div class=""header""><h1>Test Title</h1></div>");
    }

    [Fact]
    public void SectionHeading_Initializes_With_Parameters_SubHeader_Success_Tests()
    {
        var cut = RenderComponent<SectionHeader>(parameters =>
            parameters.Add(x => x.Level, 5)
                      .Add(p => p.HeadingType, HeadingType.SubHeader)
                      .AddChildContent("Test Title"));
        cut.MarkupMatches(@"<div class=""subheader""><h5>Test Title</h5></div>");
    }
}
