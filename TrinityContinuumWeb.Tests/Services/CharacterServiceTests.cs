using FluentAssertions;
using NSubstitute;
using TrinityContinuumWeb.Models;
using TrinityContinuumWeb.Services;

namespace TrinityContinuum.Tests.Services
{
    public class CharacterServiceTests
    {
        private IDataProviderService _dataProviderService;

        public CharacterServiceTests()
        {
            _dataProviderService = Substitute.For<IDataProviderService>();
        }

        private CharacterService CreateService() => new CharacterService(_dataProviderService);

        [Fact]
        public async Task GetCharacterFromId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            _dataProviderService.ReadData(Arg.Is<string>(x => x == "Characters"), Arg.Is<string>(x => x == "1.json"))
                .Returns(File.ReadAllText("Data/Characters/1.json"));

            var service = CreateService();
            var expected = new Character
            {
                Id = 1,
                Name = "Connor McCormick",
                Player = "Thomas",
                Concept = "Private Investigator",
                OriginPath = new() { Name = "Street Rat", Dots = 1 },
                RolePath = new() { Name = "Detective", Dots = 1 },
                SocietyPath = new() { Name = "Æon Trinity", Dots = 1 },
            };

            // Act
            var result = await service.GetCharacterFromId(1);

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expected, config =>
                config.Including(x => x.Id)
                      .Including(x => x.Name)
                        .Including(x => x.Player)
                        .Including(x => x.Concept)
                        .Including(x => x.OriginPath)
                        .Including(x => x.RolePath)
                        .Including(x => x.SocietyPath)
            );
        }

        [Fact]
        public async Task GetCharacterList_Success()
        {
            _dataProviderService.GetDataList(Arg.Is<string>(x => x == "Characters"))
                .Returns(["1.json"]);
            _dataProviderService.ReadData(Arg.Is<string>(x => x == "Characters"), Arg.Is<string>(x => x == "1.json"))
                .Returns(File.ReadAllText("Data/Characters/1.json"));

            // Arrange
            var service = CreateService();
            var expected = new[] { new CharacterSummary
                {
                    Id = 1,
                    Name = "Connor McCormick",
                    Player = "Thomas"
                }
            };

            // Act
            var result = await service.GetCharacterList();

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expected);
        }
    }
}
