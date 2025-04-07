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
    private const string _catalog = "Characters";

    public async Task<Character?> GetCharacterFromId(int id)
    {
        var data = await _dataProvider.ReadData(_catalog, $"{id}.json");
        var character = JsonConvert.DeserializeObject<Character>(data)!;

        return character;
    }

    public async Task<IEnumerable<CharacterSummary?>?> GetCharacterList()
    {
        var list = await _dataProvider.GetDataList(_catalog);
        var result = new List<CharacterSummary?>();
        foreach (var item in list)
        {
            var data = await _dataProvider.ReadData(_catalog, item);
            var character = JsonConvert.DeserializeObject<CharacterSummary>(data);
            result.Add(character);
        }
        return result;
    }
}
