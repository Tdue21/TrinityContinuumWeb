using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using TrinityContinuum.Models.Dtos;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.Services;
using TrinityContinuum.Services.Repositories;
using TrinityContinuum.TestData;

namespace TrinityContinuum.ApiTests;

[Trait("Category", "Integration")]
public class CharacterControllerTests(WebAppFactory factory) : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory = factory;

    [Fact]
    public async Task ListCharacters_Success_Test()
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
                            });
                        services.AddSingleton<IFileSystem>(fs);

                        var env = Substitute.For<IEnvironmentService>();
                        env.RootPath.Returns(fs.Path.Combine(fs.Directory.GetCurrentDirectory(), "data"));

                        services.AddSingleton(env);
                    });
                })
            .CreateClient();

        // Act
        var response = await client.GetAsync("/api/character/list");

        // Assert
        response.Should().NotBeNull();
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        var characters = await response.Content.ReadFromJsonAsync<CharacterSummary[]>();
        characters.Should().NotBeNull()
                       .And.HaveCount(3);
    }

    [Fact]
    public async Task ListCharacters_BadRequest_Directory_Not_Found_Test()
    {
        // Arrange
        var client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var fs = new MockFileSystem(new Dictionary<string, MockFileData>());
                        services.AddSingleton<IFileSystem>(fs);
                    });
                })
            .CreateClient();

        // Act
        var response = await client.GetAsync("/api/character/list");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        content.Should().NotBeNull();
        content.Title.Should().Be("Exception: System.IO.DirectoryNotFoundException");
    }

    [Fact]
    public async Task ListCharacters_BadRequest_Exception_Test()
    {
        // Arrange
        var client = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var fs = new MockFileSystem(new Dictionary<string, MockFileData>
                            {
                                { "/data/characters/1.json", new(CharacterData.OneJson) },
                            });
                services.AddSingleton<IFileSystem>(fs);

                var repoFactory = Substitute.For<IRepositoryFactory>();
                repoFactory.CreateRepository<Character>(default)
                    .ThrowsAsync<Exception>();

                services.AddScoped<IRepositoryFactory>(x => repoFactory);

            });
        })
            .CreateClient();

        // Act
        var response = await client.GetAsync("/api/character/list");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadFromJsonAsync<ProblemDetails>();
        content.Should().NotBeNull();
        content.Title.Should().Be("Exception: System.Exception");
    }

    [Fact]
    public async Task ListCharacters_NotFound_Test()
    {
        // Arrange
        var client = _factory.WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        var fs = new MockFileSystem(new Dictionary<string, MockFileData>());
                        fs.AddDirectory("/data/characters");
                        services.AddSingleton<IFileSystem>(fs);

                    });
                })
            .CreateClient();

        // Act
        var response = await client.GetAsync("/api/character/list");

        // Assert
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
