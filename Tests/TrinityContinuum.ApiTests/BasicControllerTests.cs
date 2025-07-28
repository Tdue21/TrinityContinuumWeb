using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TrinityContinuum.ApiTests.Infrastructure;
using TrinityContinuum.Services;
using TrinityContinuum.TestData;

namespace TrinityContinuum.ApiTests;

[Trait("Category", "Integration")]
public class BasicControllerTests(WebAppFactory factory) : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory = factory;

    [Theory]
    [InlineData("/api/character/1")]
    [InlineData("/api/character/list")]
    [InlineData("/api/powers/list")]
    public async Task Basic_Url_Success_TestsAsync(string url)
    {
        // Arrange
        Dictionary<string, MockFileData> files = new()                             {
                                { "data/characters/1.json", new(CharacterData.OneJson) },
                                { "data/characters/2.json", new(CharacterData.TwoJson) },
                                { "data/characters/3.json", new(CharacterData.ThreeJson) },
                                { "data/psi-powers.json", new(PsiPowerData.PsiPowersJson) },
                            };

        var client = _factory.WithWebHostBuilder(_ => {}, files, true)
                             .CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        
        var contentType = response.Content.Headers.ContentType?.ToString();
        contentType.Should().NotBeNull().And.Be("application/json; charset=utf-8");

    }
}
