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
public class PowersService(IRepositoryFactory factory) : IPowersService
{
    private readonly IRepositoryFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    public async Task<IEnumerable<PsiPower>> GetPsiPowers(CancellationToken cancellationToken = default)
    {
        var repository = await CreateRepositoryAsync(cancellationToken);
        var result = await repository.GetAllAsync(cancellationToken);
        return result ?? [];
    }

    private async Task<ISingleFileRepository<PsiPower>> CreateRepositoryAsync(CancellationToken cancellationToken = default)
    => await _factory.CreateSingleFileRepository<PsiPower>(cancellationToken)
        ?? throw new InvalidOperationException("Repository for PsiPowers not found.");


}
