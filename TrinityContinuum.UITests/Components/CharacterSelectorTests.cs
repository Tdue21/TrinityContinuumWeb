using AngleSharp.Html.Dom;
using FluentAssertions;
using Newtonsoft.Json;
using NSubstitute;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using TrinityContinuum.UITests.Utils;
using TrinityContinuum.WebApp.Components.Components;
using TrinityContinuumWeb.Models;
using Xunit;

namespace TrinityContinuum.UITests.Components;

public class CharacterSelectorTests : TestContext
{

    public CharacterSelectorTests()
    {
        var items = JsonConvert.SerializeObject(new CharacterSummary[]
         {
            new() { Id = 1, Name = "Test Character", Player = "Test Player" },
            new() { Id = 2, Name = "Test Character 2", Player = "Test Player 2" },
            new() { Id = 3, Name = "Test Character 3", Player = "Test Player 3" }
         });

        var messageHandler = new MockHttpMessageHandler(items, HttpStatusCode.OK);
        var client = new HttpClient(messageHandler)
        {
            BaseAddress = new Uri("https://localhost:5001/")
        };

        var factory = Substitute.For<IHttpClientFactory>();
        factory.CreateClient(Arg.Any<string>()).Returns(client);

        Services.AddSingleton(factory);
    }


    [Fact]
    public void CharacterSelector_Initializes_And_Renders_Characters_Success_Test()
    {
        var cut = RenderComponent<CharacterSelector>();
        var options = cut.FindAll("option");
       
        options.Count.Should().Be(4);
        var option = options.First(options => options.TextContent == "Select a character");
        
        option.Should().NotBeNull();
        option.Should().BeAssignableTo<IHtmlOptionElement>();
        option.As<IHtmlOptionElement>().Value.Should().Be("");
    }

    [Fact]
    public void CharacterSelector_Select_Third_Option_Test()
    {
        var cut = RenderComponent<CharacterSelector>();
        cut.Find("select").Change(2);
        cut.Instance.SelectedCharacter.Should().Be(2);
    }
}
