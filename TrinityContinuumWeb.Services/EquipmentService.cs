using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinityContinuum.Models;
using TrinityContinuumWeb.Services;

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
    Task<IEnumerable<Weapon>?> GetWeaponsList();
}

/// <summary>
/// 
/// </summary>
public class EquipmentService(IDataProviderService dataProvider) : IEquipmentService
{
    private readonly IDataProviderService _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

    public async Task<IEnumerable<Weapon>?> GetWeaponsList()
    {
        var data = await _dataProvider.ReadData("", "weapons.json");
        var result = JsonConvert.DeserializeObject<IEnumerable<Weapon>>(data);
        return result;
    }
}
