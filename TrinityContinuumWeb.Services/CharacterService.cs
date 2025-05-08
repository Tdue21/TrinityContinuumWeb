using Newtonsoft.Json;
using System.Text;
using TrinityContinuum.Models;

namespace TrinityContinuum.Services;

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

        var token = !string.IsNullOrWhiteSpace(character.Token) 
            ? await _dataProvider.ReadBinaryData(_catalog, character.Token) 
            : null;

        character.Token = (token != null)
            ? "data:image/webp;base64," + Convert.ToBase64String(token)
            : null;

        return character;
    }

    public async Task<IEnumerable<CharacterSummary?>?> GetCharacterList()
    {
        var list = await _dataProvider.GetDataList(_catalog);
        var result = new List<CharacterSummary?>();
        foreach (var item in list.Where(x => x.EndsWith(".json")))
        {
            var data = await _dataProvider.ReadData(_catalog, item);
            var character = JsonConvert.DeserializeObject<CharacterSummary>(data);
            result.Add(character);
        }
        return result;
    }
}
