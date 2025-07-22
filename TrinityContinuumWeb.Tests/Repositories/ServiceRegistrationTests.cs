using Microsoft.Extensions.DependencyInjection;
using TrinityContinuum.Services;
using TrinityContinuum.Services.Repositories;
using FluentAssertions;
using NSubstitute;
using TrinityContinuum.Models;

namespace TrinityContinuum.Tests.Repositories;

[Trait("Category", "Unit")]
public class RepositoryFactoryTests
{
	private readonly IDataProviderService _dataProviderService;
    private readonly IServiceProvider _serviceProvider;

	public RepositoryFactoryTests()
	{
        _dataProviderService = Substitute.For<IDataProviderService>();

        var services = new ServiceCollection();
		services.AddSingleton(_dataProviderService);
		services.AddRepositories();
		services.AddScoped<IRepository<TestEntity>, TestRepository>();

		_serviceProvider = services.BuildServiceProvider();
	}

    [Fact]
	public void CreateRepository_ShouldReturn_TypedRepository()
	{
        // Arrange
        var factory = _serviceProvider.GetRequiredService<IRepositoryFactory>();

        // Act
        var repository = factory.CreateRepository<TestEntity>();
		// Assert
		repository.Should().NotBeNull();
		repository.Should().BeOfType<TestRepository>();
	}

	[Fact]
	public void CreateRepository_ShouldReturn_GenericRepository()
	{
        // Arrange
        var factory = _serviceProvider.GetRequiredService<IRepositoryFactory>();

        // Act
        var repository = factory.CreateRepository<GenericEntity>();

        // Assert
        repository.Should().NotBeNull();
        repository.Should().BeOfType<GenericFileRepository<GenericEntity>>();
	}

    private class GenericEntity : BaseEntity {}
}
