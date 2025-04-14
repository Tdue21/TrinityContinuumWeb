using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinityContinuum.Server.Models;
using TrinityContinuum.Server.Services;

namespace TrinityContinuum.Tests.Server;

public class EnvironmentServiceTests
{
    private readonly string _contentRoot = Path.Combine("C:", "Test", "ContentRoot");
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
                .And.BeEquivalentTo(Path.Combine(_contentRoot, "TestData"));
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
                .And.BeEquivalentTo(Path.Combine(_contentRoot, "Data"));
    }
}
