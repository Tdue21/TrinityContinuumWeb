using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using NSubstitute;
using TrinityContinuum.Server.Models;
using TrinityContinuum.Server.Services;

namespace TrinityContinuum.Tests.Server;

[Trait("Category", "Unit")]
public class EnvironmentServiceTests
{
    private readonly string _contentRoot;
    private readonly IFileSystem _fileSystem;

    public EnvironmentServiceTests()
    {
        _fileSystem = new MockFileSystem();
        _contentRoot = _fileSystem.Path.Combine("C:", "Test", "ContentRoot");
    }

    [Fact]
    public void TestRootPath_HasApplicationSettings_Success()
    {


        // Arrange
        var environment = Substitute.For<IWebHostEnvironment>();
        environment.ContentRootPath.Returns(_contentRoot);

        var settings = Substitute.For<IOptions<ApplicationSettings>>();
        settings.Value.Returns(new ApplicationSettings { DataFolder = "TestData" });

        var service = new EnvironmentService(environment, settings);
        // Act
        var rootPath = service.RootPath;
        // Assert
        rootPath.Should().NotBeNullOrEmpty()
                .And.BeEquivalentTo(_fileSystem.Path.Combine(_contentRoot, "TestData"));
    }

    [Fact]
    public void TestRootPath_NoApplicationSettings_Success()
    {
        // Arrange
        var environment = Substitute.For<IWebHostEnvironment>();
        environment.ContentRootPath.Returns(_contentRoot);

        var settings = Substitute.For<IOptions<ApplicationSettings>>();
        settings.Value.Returns(new ApplicationSettings { DataFolder = null });

        var service = new EnvironmentService(environment, settings);
        // Act
        var rootPath = service.RootPath;
        // Assert
        rootPath.Should().NotBeNullOrEmpty()
                .And.BeEquivalentTo(_fileSystem.Path.Combine(_contentRoot, "Data"));
    }
}
