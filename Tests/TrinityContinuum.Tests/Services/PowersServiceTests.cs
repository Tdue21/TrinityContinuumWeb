using FluentAssertions;
using NSubstitute;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.Services;
using TrinityContinuum.Services.Repositories;

namespace TrinityContinuum.Tests.Services;

[Trait("Category", "Unit")]
public class PowersServiceTests
{
    private readonly PowersService _powersService;
    private readonly IDataProviderService _dataProviderService;
    private readonly IRepositoryFactory _factory;

    public PowersServiceTests()
    {
        _dataProviderService = Substitute.For<IDataProviderService>();
        _dataProviderService.ReadData(
                                Arg.Is<string>(x => x == string.Empty), 
                                Arg.Is<string>(x => x == "psi-powers.json"), 
                                default)
                            .Returns(Task.FromResult(TestData));
        var repository = new SingleFileRepository<PsiPower>(_dataProviderService);
        repository.Initialize(default).GetAwaiter().GetResult();
        _factory = Substitute.For<IRepositoryFactory>();
        _factory.CreateSingleFileRepository<PsiPower>().Returns(repository);

        _powersService = new PowersService(_factory);

    }

    [Fact]
    public async Task GetPsiPowers_Success_Test()
    {
        // Act
        var result = await _powersService.GetPsiPowers(CancellationToken.None);

        // Assert
        result.Should().NotBeNull().And.HaveCount(5);
    }


    private const string TestData = """
[        
    {
        "Aptitude": "Biokinesis",
        "Mode": "Adaptation",
        "Name": "Resist",
        "Description": "The psion mitigates sources of harm coming from within her body.",
        "System": "Success reduces the damage rating of hazards such as toxins, irritants, and diseases within the psion's body by an amount equal to her Mode dots. If the power reduces the damage rating to zero the character can either internally neutralize the substance, or expel it intact from her body. If the damage rating of a hazard is not reduced to zero, it continues to damage the psion at the reduced rate.",
        "Dots": 1,
        "Cost": "0",
        "Source": "ÆON",
        "Page": 211
    },
    {
        "Aptitude": "Biokinesis",
        "Mode": "Adaptation",
        "Name": "Acclimatize",
        "Description": "A psion can adjust his physiology to best suit the environment.",
        "System": "The psion suffers no penalties from increased or decreased gravity that is less than extreme gravity and can also ignore all environmental indirect damage with the Continuous (hour) tag as well as all environmental indirect damage with the Continuous (minute) tag that does not also possess the Aggravated tag. The psion can reduce the damage rating of other forms of environmental indirect damage by one per success spent.",
        "Dots": 2,
        "Cost": "0",
        "Source": "ÆON",
        "Page": 211
    },
    {
        "Aptitude": "Biokinesis",
        "Mode": "Adaptation",
        "Name": "Metabolic Control",
        "Description": "The biokinetic controls the speed of her body's processes. While this can reduce harmful conditions, it also works to repair damage.",
        "System": "If successful, the character changes the rate of one aspect of her physiology. For functions that take a defined time, such as healing, holding her breath, or going without water, she multiplies (or divides) the time by Mode dots + successes. For processes not measured in time, such as Initiative, the character either adds (or subtracts) Mode dots/2 to her total.",
        "Dots": 3,
        "Cost": "1",
        "Source": "ÆON",
        "Page": 211
    },
    {
        "Aptitude": "Clairsentience",
        "Mode": "Psychocognition",
        "Name": "Alertness",
        "Description": "The bulk of the clairsentient's attention remains focused on the present, but a small fragment watches the immediate future.",
        "System": "The clairsentient increases her Defense by an amount equal to her Mode dots. The Storyguide should also warn the character if her action, the actions of others, or the environment would create negative consequences for the clear in subsequent rounds. This includes incoming attacks, but also if an earthquake is about to strike the building she is in (or about to enter).",
        "Dots": 1,
        "Cost": "0",
        "Source": "ÆON",
        "Page": 223
    },
    {
        "Aptitude": "Clairsentience",
        "Mode": "Psychocognition",
        "Name": "Least Resistance",
        "Description": "At moments of decision the clairsentient glimpses across all possible choices to see which is the easiest path to take or what actions he should avoid taking.",
        "System": "The character knows the quickest path to his destination, such as the exit to a building, the nearest fire alarm, or where he left an object he lost. If his actions require a roll, he gains a pool equal to successes that he can use as an Enhancement to any necessary rolls.",
        "Dots": 2,
        "Cost": "1",
        "Source": "ÆON",
        "Page": 223
    }
]
""";
}
