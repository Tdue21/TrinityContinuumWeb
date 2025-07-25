using Newtonsoft.Json;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.Services.Repositories;

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
public class PowersService(ISingleFileRepository<PsiPower> repository) : IPowersService
{
    private readonly ISingleFileRepository<PsiPower> _repository = repository ?? throw new ArgumentNullException(nameof(repository));

    public async Task<IEnumerable<PsiPower>> GetPsiPowers(CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAllAsync(cancellationToken);
        return result ?? [];
    }
}
