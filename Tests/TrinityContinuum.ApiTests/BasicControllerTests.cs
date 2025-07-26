using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
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
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var fs = new MockFileSystem(new Dictionary<string, MockFileData>
                            {
                                { "/data/characters/1.json", new(CharacterData.OneJson) },
                                { "/data/characters/2.json", new(CharacterData.TwoJson) },
                                { "/data/characters/3.json", new(CharacterData.ThreeJson) },
                                { "/data/psi-powers.json", new("") },
                            });
                var env = Substitute.For<IEnvironmentService>();
                env.RootPath.Returns(fs.Path.Combine(fs.Directory.GetCurrentDirectory(), "data"));

                services.AddSingleton<IFileSystem>(fs);
                services.AddSingleton(env);
            });
        }).CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        
        var contentType = response.Content.Headers.ContentType?.ToString();
        contentType.Should().NotBeNull().And.Be("application/json; charset=utf-8");

    }
}
