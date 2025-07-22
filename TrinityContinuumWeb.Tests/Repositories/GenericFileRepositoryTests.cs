using Xunit;
using FluentAssertions;
using NSubstitute;
using TrinityContinuum.Services;
using TrinityContinuum.Services.Repositories;
using System.Threading.Tasks;

namespace TrinityContinuum.Tests.Repositories;

[Trait("Category", "Unit")]
public class GenericFileRepositoryTests
{
    IRepository<TestEntity> _repository;
    IDataProviderService _dataProvider;

    public GenericFileRepositoryTests()
    {
        _dataProvider = Substitute.For<IDataProviderService>();

        _dataProvider.GetDataList(Arg.Any<string>(), Arg.Any<CancellationToken>())
                    .Returns(Task.FromResult<IEnumerable<string>>(new List<string> { "1.json", "2.json", "3.json" }));

        _dataProvider.ReadData(Arg.Any<string>(), Arg.Is<string>(x => x.Equals($"1.json")), default)
                    .Returns(Task.FromResult("""{"Id":1,"Name":"Test Entity 1"}"""));

        _dataProvider.ReadData(Arg.Any<string>(), Arg.Is<string>(x => x.Equals($"2.json")), default)
                    .Returns(Task.FromResult("""{"Id":2,"Name":"Test Entity 2"}"""));

        _dataProvider.ReadData(Arg.Any<string>(), Arg.Is<string>(x => x.Equals($"3.json")), default)
                    .Returns(Task.FromResult("""{"Id":3,"Name":"Test Entity 3"}"""));

        _repository = new GenericFileRepository<TestEntity>(_dataProvider);
        _repository.Initialize(CancellationToken.None).GetAwaiter().GetResult();
    }

    [Fact]
    public async Task GetAsync_Success_Test()
    {
        // Arrange
        var expected = new TestEntity { Id = 1, Name = "Test Entity 1" };

        // Act
        var actual = await _repository.GetAsync(1, CancellationToken.None);

        // Assert
        actual.Should().NotBeNull().And.BeEquivalentTo(expected,
                config => config.Including(x => x.Id)
                                .Including(x => x.Name));
    }

    [Fact]
    public async Task GetAsync_Failed_Test()
    {
        // Act
        var actual = await _repository.GetAsync(4, CancellationToken.None);

        // Assert
        actual.Should().BeNull();
    }

    [Fact]
    public async Task GetAsync_Throws_Exception_Test()
    {
        // Arrange
        Func<Task> act = async () => await _repository.GetAsync(0, CancellationToken.None);
        // Assert
        await act.Should().ThrowAsync<ArgumentOutOfRangeException>()
            .WithMessage("Id must be greater than zero. (Parameter 'id')");
    }

    [Fact]
    public async Task GetAllAsync_Success_Test()
    {
        // Arrange
        var expected = new List<TestEntity>
        {
            new() { Id = 1, Name = "Test Entity 1" },
            new() { Id = 2, Name = "Test Entity 2" },
            new() { Id = 3, Name = "Test Entity 3" }
        };

        // Act
        var actual = await _repository.GetAllAsync(CancellationToken.None);

        // Assert
        actual.Should().NotBeNull()
              .And.HaveCount(3)
              .And.BeEquivalentTo(expected,
                config => config.Including(x => x.Id)
                                .Including(x => x.Name));
    }

    [Fact]
    public async Task AddAsync_Throws_Exception_On_Null_Test()
    {
        // Arrange
        Func<Task> act = async () => await _repository.AddAsync(null!, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entity')");
    }

    [Fact]
    public async Task AddSync_Throw_Exception_Existing_Id_Test()
    {
        // Arrange
        var entity = new TestEntity { Id = 1, Name = "Test Entity 1" };
        Func<Task> act = async () => await _repository.AddAsync(entity, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Entity with Id 1 already exists.");
    }

    [Fact]
    public async Task AddSync_Success_Test()
    {
        // Arrange
        var entity = new TestEntity { Id = 4, Name = "Test Entity 4" };

        // Act
        await _repository.AddAsync(entity, CancellationToken.None);
        var actual = await _repository.GetAsync(4, CancellationToken.None);

        // Assert
        await _dataProvider.Received(1).WriteData(
            Arg.Any<string>(),
            Arg.Is<string>(x => x.Equals("4.json")),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>());

        actual.Should().NotBeNull()
              .And.BeEquivalentTo(entity,
                config => config.Including(x => x.Id)
                                .Including(x => x.Name));
    }


    [Fact]
    public async Task UpdateAsync_Throws_Exception_On_Null_Test()
    {
        // Arrange
        Func<Task> act = async () => await _repository.UpdateAsync(null!, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entity')");
    }

    [Fact]
    public async Task UpdateSync_Throw_Exception_Invalid_Id_Test()
    {
        // Arrange
        var entity = new TestEntity { Id = -3, Name = "Test Entity 1" };
        Func<Task> act = async () => await _repository.UpdateAsync(entity, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentOutOfRangeException>()
            .WithMessage("Entity Id must be greater than zero. (Parameter 'entity')");
    }

    [Fact]
    public async Task UpdateAsync_Throws_Exception_Entity_DoesNotExist_Test()
    {
        // Arrange
        var entity = new TestEntity { Id = 4, Name = "Test Entity 4" };
        Func<Task> act = async () => await _repository.UpdateAsync(entity, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Entity with Id 4 does not exist.");
    }

    [Fact()]
    public async Task UpdateAsync_Success_Test()
    {
        // Arrange
        var entity = new TestEntity { Id = 2, Name = "Updated Entity" };

        // Act
        await _repository.UpdateAsync(entity, CancellationToken.None);
        var actual = await _repository.GetAsync(2, CancellationToken.None);

        // Assert
        await _dataProvider.Received(1).WriteData(
            Arg.Any<string>(),
            Arg.Is<string>(x => x.Equals("2.json")),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>());

        actual.Should().NotBeNull()
              .And.BeEquivalentTo(entity,
                config => config.Including(x => x.Id)
                                .Including(x => x.Name));
    }


    [Fact()]
    public async Task UpdateAsync_Failure_TestAsync()
    {
        const string message = "Some exception occurred during file write.";

        _dataProvider.WriteData(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), default)
                .Returns(Task.FromException(new IOException(message)));

        // Arrange
        var entity = new TestEntity { Id = 2, Name = "Test Entity 2" };
        Func<Task> act = async () => await _repository.UpdateAsync(entity, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<IOException>()
            .WithMessage(message);
    }

    [Fact]
    public async Task DeleteAsync_Throws_Exception_On_Null_Test()
    {
        // Arrange
        Func<Task> act = async () => await _repository.DeleteAsync(null!, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentNullException>()
            .WithMessage("Value cannot be null. (Parameter 'entity')");
    }

    [Fact]
    public async Task DeleteSync_Throw_Exception_Invalid_Id_Test()
    {
        // Arrange
        var entity = new TestEntity { Id = -3, Name = "Test Entity 1" };
        Func<Task> act = async () => await _repository.DeleteAsync(entity, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentOutOfRangeException>()
            .WithMessage("Entity Id must be greater than zero. (Parameter 'id')");
    }

    [Fact]
    public async Task DeleteAsync_Throws_Exception_Entity_DoesNotExist_Test()
    {
        // Arrange
        var entity = new TestEntity { Id = 4, Name = "Test Entity 4" };
        Func<Task> act = async () => await _repository.DeleteAsync(entity, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage($"Entity with Id 4 does not exist.");
    }

    [Fact()]
    public async Task DeleteAsync_FromId_Success_TestAsync()
    {
        // Act
        await _repository.DeleteAsync(2, CancellationToken.None);

        // Assert
        _dataProvider.Received(1).DeleteData(
            Arg.Any<string>(),
            Arg.Is<string>(x => x.Equals("2.json")));
    }

    [Fact()]
    public async Task DeleteAsync_FromEntity_Success_TestAsync()
    {
        // Arrange
        var entity = new TestEntity { Id = 2, Name = "Test Entity 2" };
        
        // Act
        await _repository.DeleteAsync(entity, CancellationToken.None);

        // Assert
        _dataProvider.Received(1).DeleteData(
            Arg.Any<string>(),
            Arg.Is<string>(x => x.Equals("2.json")));
    }

    [Fact()]
    public async Task ExistsAsync_Id_Exists()
    {
        // Act
        var result = await _repository.ExistsAsync(2, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }

    [Fact()]
    public async Task ExistsAsync_Id_Does_Not_Exist()
    {
        // Act
        var result = await _repository.ExistsAsync(4, CancellationToken.None);

        // Assert
        result.Should().BeFalse();
    }

    [Fact()]
    public async Task CountAsync_Should_Return_Count_3_TestAsync()
    {
        // Act
        var result = await _repository.CountAsync(CancellationToken.None);

        // Assert
        result.Should().Be(3);
    }

    [Fact()]
    public async Task CountAsync_Should_Return_Count_2_TestAsync()
    {
        // Arrange
        await _repository.DeleteAsync(3, CancellationToken.None);

        // Act
        var result = await _repository.CountAsync(CancellationToken.None);

        // Assert
        result.Should().Be(2);
    }

    [Fact()]
    public async Task CountAsync_Should_Return_Count_4_TestAsync()
    {
        // Arrange
        var entity = new TestEntity { Id = 4, Name = "Test Entity 4" };
        await _repository.AddAsync(entity, CancellationToken.None);

        // Act
        var result = await _repository.CountAsync(CancellationToken.None);

        // Assert
        result.Should().Be(4);
    }

    [Fact()]
    public async Task InitializeTest()
    {
        // Arrange
        _dataProvider.ClearReceivedCalls();
        _repository = new GenericFileRepository<TestEntity>(_dataProvider);

        // Act
        await _repository.Initialize(CancellationToken.None);

        // Assert
        await _dataProvider.Received(1).GetDataList(
            Arg.Any<string>(),
            Arg.Any<CancellationToken>());

        await _dataProvider.Received(3).ReadData(
            Arg.Any<string>(),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>());
    }
}
