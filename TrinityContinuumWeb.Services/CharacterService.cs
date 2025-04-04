using System.Text.Json;
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
        var options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
        options.PropertyNameCaseInsensitive = true;
        options.AllowTrailingCommas = true;
        options.ReadCommentHandling = JsonCommentHandling.Skip;
        var data = await _dataProvider.ReadFile(id);
        var character = JsonSerializer.Deserialize<Character>(data, options);
        return character;
    }

    public async Task<IEnumerable<Character?>?> GetCharacterList()
    {
        var data = await _dataProvider.ReadFiles();
        var result = data.Where(x => !string.IsNullOrWhiteSpace(x))
                         .Select(x => JsonSerializer.Deserialize<Character>(x))
                         .ToArray();
        return result;
    }
}
