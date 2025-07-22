using Newtonsoft.Json;
using TrinityContinuum.Models;

namespace TrinityContinuum.Services;

/// <summary>
/// 
/// </summary>
public interface IPowersService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<PsiPower>> GetPsiPowers(CancellationToken cancellationToken = default);
}

/// <summary>
/// 
/// </summary>
/// <param name="dataProvider"></param>
public class PowersService(IDataProviderService dataProvider) : IPowersService
{
    private readonly IDataProviderService _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

    public async Task<IEnumerable<PsiPower>> GetPsiPowers(CancellationToken cancellationToken = default)
    {
        var data = await _dataProvider.ReadData("", "psi-powers.json", cancellationToken);
        var result = JsonConvert.DeserializeObject<IEnumerable<PsiPower>>(data);
        return result ?? [];
    }
}
