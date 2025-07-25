using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using NSubstitute;
using TrinityContinuum.Services;
using TrinityContinuum.TestData;

namespace TrinityContinuum.Tests.Services
{
    [Trait("Category", "Unit")]
    [Trait("Dependency", "File System")]
    public class FileProviderServiceTests
    {
        private readonly IEnvironmentService _environmentService;
        private readonly MockFileSystem _fileSystem;

        public FileProviderServiceTests()
        {
            _environmentService = Substitute.For<IEnvironmentService>();
            _environmentService.RootPath.Returns("Data");

            _fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { "Data/Characters/1.json", new MockFileData(CharacterData.OneJson)  },
                { "Data/Characters/2.json", new MockFileData(CharacterData.TwoJson)  }

            }, new MockFileSystemOptions
            {
                CurrentDirectory = "C:\\TestData"
            });
        }

        private FileProviderService CreateService(IFileSystem? fileSystem = null) 
            => new FileProviderService(fileSystem ?? _fileSystem, _environmentService);


        [Fact]
        public async Task ReadData_Success()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.ReadData("Characters", "1.json");

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(CharacterData.OneJson);
        }


        [Fact]
        public async Task ReadData_Throw_FileNotFoundException_Success()
        {
            // Arrange
            var service = CreateService();

            // Act
            await service.Invoking(x => x.ReadData("Characters", "11.json"))
                .Should().ThrowAsync<FileNotFoundException>();
        }

        [Fact]
        public async Task WriteData_Success_Saved_Backup_And_File()
        {
            // Arrange
            var service = CreateService();

            // Act
            await service.WriteData("Characters", "1.json", CharacterData.OneJson, default);

            // Assert
            _fileSystem.AllFiles.Should()
                .HaveCount(3).And
                .Contain(@"C:\TestData\Data\Characters\1.json").And
                .ContainMatch(@"C:\TestData\Data\Characters\1.json-*.bak");
        }

        [Fact]
        public async Task GetDataList_ListFiles_Success()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.GetDataList("Characters");

            // Assert
            result.Should().NotBeNull().And.HaveCount(2).And.BeEquivalentTo(["1.json", "2.json"]);
        }
    }
}
