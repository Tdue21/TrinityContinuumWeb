using Newtonsoft.Json;
using TrinityContinuumWeb.Models;

namespace TrinityContinuumWeb.Services;

/// <summary>
/// 
/// </summary>
public interface ICharacterService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Character?> GetCharacterFromId(int id);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CharacterSummary?>?> GetCharacterList();
}

/// <summary>
/// 
/// </summary>
public class CharacterService(IDataProviderService dataProvider) : ICharacterService
{
    private readonly IDataProviderService _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    
        
    public async Task<Character?> GetCharacterFromId(int id)
    {
        var data = await _dataProvider.ReadFile($"Characters/{id}");
        var character = JsonConvert.DeserializeObject<Character>(data)!;

        return character;
    }

    public async Task<IEnumerable<CharacterSummary?>?> GetCharacterList()
    {
        var data = await _dataProvider.ReadFile("characters");
        var result = JsonConvert.DeserializeObject<IEnumerable<CharacterSummary>>(data);
        return result;
    }
}
