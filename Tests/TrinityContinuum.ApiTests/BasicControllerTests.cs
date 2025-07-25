using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
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
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var fs = new MockFileSystem(new Dictionary<string, MockFileData>
                            {
                                { "/Data/Characters/1.json", new(CharacterData.OneJson) },
                                { "/Data/Characters/2.json", new(CharacterData.TwoJson) },
                                { "/Data/Characters/3.json", new(CharacterData.ThreeJson) },
                                { "/Data/psi-powers.json", new("") },
                            });
                services.AddSingleton<IFileSystem>(fs);
            });
        })
            .CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        
        var contentType = response.Content.Headers.ContentType?.ToString();
        contentType.Should().NotBeNull().And.Be("application/json; charset=utf-8");

    }
}
