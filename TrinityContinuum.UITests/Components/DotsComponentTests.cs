using FluentAssertions;
using System.Linq;
using TrinityContinuum.WebApp.Components.Components;

namespace TrinityContinuum.UITests.Components;

/// <summary>
/// These tests are written entirely in C#.
/// Learn more at https://bunit.dev/docs/getting-started/writing-tests.html#creating-basic-tests-in-cs-files
/// </summary>
public class DotsComponentTests : TestContext
{
    [Fact]
    public void Dots_Click_Should_Be_Specific_Value_Success_Tests()
    {
        var cut = RenderComponent<Dots>();
        var index = 0;
        do
        {
            var dots = cut.FindAll(".dot");
            dots.Skip(index).First().Click();
            cut.Instance.Value.Should().Be(index + 1);
            index++;
        } while (index < 5);
    }

    [Fact]
    public void Dots_Readonly_Click_Value_Not_Changing_Success_Tests()
    {
        var cut = RenderComponent<Dots>(parameters => parameters
            .Add(p => p.MaxValue, 5)
            .Add(p => p.Value, 2)
            .Add(p => p.ReadOnly, true));

        var index = 0;
        do
        {
            var dots = cut.FindAll(".dot");
            dots.Skip(index).First().Click();
            cut.Instance.Value.Should().Be(2);
            index++;
        } while (index < 5);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    public void Dots_Number_Of_Dots_Equals_MaxValue_Success_Tests(int maxValue)
    {
        var cut = RenderComponent<Dots>(parameters => parameters.Add(p => p.MaxValue, maxValue));
        var dots = cut.FindAll(".dot");
        dots.Count.Should().Be(maxValue);
    }
}
