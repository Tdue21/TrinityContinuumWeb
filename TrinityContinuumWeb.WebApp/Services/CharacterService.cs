using Newtonsoft.Json;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Clients;

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
}
