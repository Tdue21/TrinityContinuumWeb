using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrinityContinuum.ApiTests.Infrastructure;
using TrinityContinuum.Identity;
using TrinityContinuum.Identity.Models;

namespace TrinityContinuum.ApiTests;

[Trait("Category", "Integration")]
public class AuthControllerTests(WebAppFactory factory) : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    [Fact]
    public async Task RegisterNewUser_And_Login_Success_Test()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var registerDto = new RegisterUserDto("test@nowhere.org", "TestPassword123!");
        var response = await client.PostAsJsonAsync("/api/auth/register", registerDto);
        
        // Assert
        response.EnsureSuccessStatusCode();

        // Act 2
        var loginDto = new LoginUserDto("test@nowhere.org", "TestPassword123!");
        response = await client.PostAsJsonAsync("/api/auth/login", loginDto);

        // Assert 2
        response.EnsureSuccessStatusCode();

        // Assert 3
        var token = await response.Content.ReadFromJsonAsync<TokenResponse>();
        token.Should().NotBeNull();
        token.Token.Should().NotBeNullOrEmpty();
    }
}

public record TokenResponse(string Token);