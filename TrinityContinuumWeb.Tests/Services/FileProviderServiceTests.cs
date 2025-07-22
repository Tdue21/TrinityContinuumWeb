using FluentAssertions;
using NSubstitute;
using TrinityContinuum.Services;

namespace TrinityContinuum.Tests.Services
{
    [Trait("Category", "Unit")]
    [Trait("Dependency", "File System")]
    public class FileProviderServiceTests
    {
        private const string OneJson = """
            {
            	"id": 1,
            	"name": "Connor McCormick",
            	"player": "Thomas",
            	"concept": "Private Investigator",

            	"originPath": {
            		"name": "Street Rat",
            		"dots": 1
            	},
            	"rolePath": {
            		"name": "Detective",
            		"dots": 1
            	},
            	"societyPath": {
            		"name": "Æon Trinity",
            		"dots": 1
            	}
            }
            """;


        private IEnvironmentService _environmentService;

        public FileProviderServiceTests()
        {
            _environmentService = Substitute.For<IEnvironmentService>();
            _environmentService.RootPath.Returns("Data");
        }

        private FileProviderService CreateService() => new FileProviderService(_environmentService);


        [Fact]
        public async Task ReadData_Success()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.ReadData("Characters", "1.json");

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(OneJson);
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

        //[Fact]
        //public async Task WriteData_StateUnderTest_ExpectedBehavior()
        //{
        //    // Arrange
        //    var service = this.CreateService();
        //    string catalog = null;
        //    string id = null;
        //    string content = null;
        //    CancellationToken cancellationToken = default(global::System.Threading.CancellationToken);

        //    // Act
        //    await service.WriteData(
        //        catalog,
        //        id,
        //        content,
        //        cancellationToken);

        //    // Assert
        //    Assert.True(false);
        //}

        [Fact]
        public async Task GetDataList_ListFiles_Success()
        {
            // Arrange
            var service = CreateService();

            // Act
            var result = await service.GetDataList("Characters");

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(["1.json"]);
        }
    }
}
