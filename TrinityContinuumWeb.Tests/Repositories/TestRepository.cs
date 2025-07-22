using TrinityContinuum.Services;
using TrinityContinuum.Services.Repositories;

namespace TrinityContinuum.Tests.Repositories;

/// <summary>
/// Test repository class for testing the abstract file repository.
/// </summary>
/// <param name="dataProvider"></param>
internal class TestRepository(IDataProviderService dataProvider) : GenericFileRepository<TestEntity>(dataProvider)
{ }
