using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TrinityContinuum.Services.Repositories;

namespace TrinityContinuum.Tests.Repositories;

[Trait("Category", "Unit")]
public class RepositoryUtilitiesTests
{
    [Fact]
    public void GetCatalogName_ShouldReturnCorrectName_ForTypeName()
    {
        // Arrange
        var expected = "testentity";
        
        // Act
        var result = RepositoryUtilities.GetCatalogName<TestEntity>();

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public void GetCatalogName_ShouldReturnCorrectName_ForTableAttribute()
    {
        // Arrange
        var expected = "test-entities";
        
        // Act
        var result = RepositoryUtilities.GetCatalogName<TestEntityWithTableAttribute>();
        // Assert
        result.Should().Be(expected);
    }

}
