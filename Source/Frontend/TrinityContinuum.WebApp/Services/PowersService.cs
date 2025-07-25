using TrinityContinuum.Models.Entities;
using TrinityContinuum.WebApp.Clients;

namespace TrinityContinuum.WebApp.Services;

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
    Task<IEnumerable<PsiPower>?> GetPowers(CancellationToken cancellationToken);
}

/// <summary>
/// 
/// </summary>
public class PowersService(IApiClient apiClient) : IPowersService
{
    private readonly IApiClient _apiClient = apiClient;

    public async Task<IEnumerable<PsiPower>?> GetPowers(CancellationToken cancellationToken)
    {
        var response = await _apiClient.GetPowers(cancellationToken);
        return response?.ToList() ?? [];
    }
}
