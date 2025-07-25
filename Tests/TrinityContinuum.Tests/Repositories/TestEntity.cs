using System.ComponentModel.DataAnnotations.Schema;
using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Tests.Repositories;

/// <summary>
/// Test entity class for the repository.
/// </summary>
internal class TestEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}

[Table("test-entities")]
internal class TestEntityWithTableAttribute : BaseEntity
{}
