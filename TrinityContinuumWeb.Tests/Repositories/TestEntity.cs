using TrinityContinuum.Models;

namespace TrinityContinuum.Tests.Repositories;

/// <summary>
/// Test entity class for the repository.
/// </summary>
internal class TestEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
