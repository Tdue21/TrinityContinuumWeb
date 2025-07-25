using TrinityContinuum.Models.Dtos;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.WebApp.Clients;
using TrinityContinuum.WebApp.Models;

namespace TrinityContinuum.WebApp.Services;

/// <summary>
/// 
/// </summary>
public interface ICharacterService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<List<CharacterSummary>> GetCharactersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Character?> GetCharacterAsync(int id, CancellationToken cancellationToken = default);


    /// <summary>
    /// 
    /// </summary>
    /// <param name="stamina"></param>
    /// <param name="hasEndurance"></param>
    /// <returns></returns>
    IEnumerable<InjuryLevel> CalculateInjuryLevels(int stamina, bool hasEndurance);
    
}

/// <summary>
/// 
/// </summary>
/// <param name="apiClient"></param>
public class CharacterService(IApiClient apiClient) : ICharacterService
{
    private readonly IApiClient _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));

    public async Task<List<CharacterSummary>> GetCharactersAsync(CancellationToken cancellationToken = default)
    {
        var response = await _apiClient.GetCharacters(cancellationToken);
        return response?.ToList() ?? [];
    }

    public async Task<Character?> GetCharacterAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await _apiClient.GetCharacter(id, cancellationToken);
        return response;
    }

    public IEnumerable<InjuryLevel> CalculateInjuryLevels(int stamina, bool hasEndurance)
    {
        var levels = new List<InjuryLevel> {
                new(0, "Bruised", "+1"),
                new(1, "Injured", "+2"),
                new(2, "Maimed",  "+4")
            };

        if (stamina >= 3)
        {
            levels.Add(new(1, "Injured", "+2"));
        }

        if (stamina >= 5)
        {
            levels.Add(new(0, "Bruised", "+1"));
        }

        if (hasEndurance)
        {
            levels.Add(new(0, "Bruised", "+1"));
        }

        return levels.OrderBy(x => x.Order).ToList();
    }
}
