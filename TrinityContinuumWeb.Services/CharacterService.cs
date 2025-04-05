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
    Task<IEnumerable<Character?>?> GetCharacterList();
}

/// <summary>
/// 
/// </summary>
public class CharacterService(IDataProviderService dataProvider) : ICharacterService
{
    private readonly IDataProviderService _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
    
        
    public async Task<Character?> GetCharacterFromId(int id)
    {
        var data = await _dataProvider.ReadFile(id);
        var character = JsonConvert.DeserializeObject<Character>(data)!;

        return character;
    }

    public async Task<IEnumerable<Character?>?> GetCharacterList()
    {
        var data = await _dataProvider.ReadFiles();
        var result = data.Where(x => !string.IsNullOrWhiteSpace(x))
                         .Select(JsonConvert.DeserializeObject<Character>)
                         .ToArray();
        return result;
    }
}
