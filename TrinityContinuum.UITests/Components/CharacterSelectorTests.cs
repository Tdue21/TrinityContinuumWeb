using AngleSharp.Html.Dom;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using TrinityContinuum.Models;
using TrinityContinuum.UITests.Utils;
using TrinityContinuum.WebApp.Components.Shared;
using TrinityContinuum.WebApp.Services;
using Xunit;

namespace TrinityContinuum.UITests.Components;

public class CharacterSelectorTests : TestContext
{

    public CharacterSelectorTests()
    {
        List<CharacterSummary> items = [
            new() { Id = 1, Name = "Test Character", Player = "Test Player" },
            new() { Id = 2, Name = "Test Character 2", Player = "Test Player 2" },
            new() { Id = 3, Name = "Test Character 3", Player = "Test Player 3" }
         ];

        var service = Substitute.For<ICharacterService>();
        service.GetCharactersAsync(default).Returns(items);
        Services.AddSingleton(service);
    }

    [Fact]
    public void CharacterSelector_Initializes_And_Renders_Characters_Success_Test()
    {
        var cut = RenderComponent<CharacterSelector>();
        var options = cut.FindAll("li");
       
        options.Count.Should().Be(3);

        var option = options.First(); 
        option.Should().NotBeNull();
        option.Should().BeAssignableTo<IHtmlListItemElement>();
    }

    [Fact]
    public void CharacterSelector_Select_Third_Option_Test()
    {
        var cut = RenderComponent<CharacterSelector>();
        var anchor = (cut.Find("#char-anchor-3") as IHtmlAnchorElement);
        anchor.Should().NotBeNull();
        anchor.PathName.Should().Be("/sheet/3");
    }
}
