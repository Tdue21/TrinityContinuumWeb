using FluentAssertions;
using NSubstitute;
using TrinityContinuum.Models.Dtos;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.Services;
using TrinityContinuum.Services.Repositories;

namespace TrinityContinuum.Tests.Services
{
    [Trait("Category", "Unit")]
    public class CharacterServiceTests
    {
        private readonly IDataProviderService _dataProvider;
        private readonly IRepository<Character> _characterRepository;
        private readonly IRepositoryFactory _factory;

        public CharacterServiceTests()
        {
            _dataProvider = Substitute.For<IDataProviderService>();
            
            _dataProvider.GetDataList(Arg.Any<string>(), Arg.Any<CancellationToken>())
                        .Returns(Task.FromResult<IEnumerable<string>>(new List<string> { "1.json" }));
            
            _dataProvider.ReadData(Arg.Is<string>(x => x == "Character"), Arg.Is<string>(x => x == "1.json"))
                .Returns(File.ReadAllText("Data/Characters/1.json"));

            _characterRepository = new CharacterRepository(_dataProvider);
            _characterRepository.Initialize(CancellationToken.None).GetAwaiter().GetResult();

            _factory = Substitute.For<IRepositoryFactory>();
            _factory.CreateRepository<Character>().Returns(_characterRepository);
        }

        [Fact]
        public async Task GetCharacterFromId_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
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
            var service = new CharacterService(_factory);
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
            // Arrange
            var expected = new[] { new CharacterSummary
                {
                    Id = 1,
                    Name = "Connor McCormick",
                    Player = "Thomas"
                }
            };

            // Act
            var service = new CharacterService(_factory);
            var result = await service.GetCharacterList();

            // Assert
            result.Should().NotBeNull().And.BeEquivalentTo(expected);
        }
    }
}
