using Newtonsoft.Json;
using TrinityContinuum.Models;

namespace TrinityContinuum.Services;

/// <summary>
/// 
/// </summary>
public interface IEquipmentService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Weapon>?> GetWeaponsList(CancellationToken cancellationToken = default);
}

/// <summary>
/// 
/// </summary>
public class EquipmentService(IDataProviderService dataProvider) : IEquipmentService
{
    private readonly IDataProviderService _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

    public async Task<IEnumerable<Weapon>?> GetWeaponsList(CancellationToken cancellationToken = default)
    {
        var data = await _dataProvider.ReadData("", "weapons.json", cancellationToken);
        var result = JsonConvert.DeserializeObject<IEnumerable<Weapon>>(data);
        return result;
    }
}
