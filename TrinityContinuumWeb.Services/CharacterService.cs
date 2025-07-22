using TrinityContinuum.Models.Dtos;
using TrinityContinuum.Models.Entities;
using TrinityContinuum.Services.Repositories;

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
    Task<Character?> GetCharacterFromId(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CharacterSummary?>?> GetCharacterList(CancellationToken cancellationToken = default);
}

/// <summary>
/// 
/// </summary>
public class CharacterService(IRepositoryFactory factory) : ICharacterService
{
    private readonly IRepositoryFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    public async Task<Character?> GetCharacterFromId(int id, CancellationToken cancellationToken = default)
    {
        var repository = await CreateRepositoryAsync();
        return await repository.GetAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<CharacterSummary?>?> GetCharacterList(CancellationToken cancellationToken = default)
    {
        var repository = await CreateRepositoryAsync();
        var list = await repository.GetAllAsync(cancellationToken);
        if (list == null || !list.Any())
        {
            return null;
        }

        var result = list.Select(x => new CharacterSummary
        {
            Id = x.Id,
            Name = x.Name,
            Player = x.Player,
        });

        return result;
    }
    private async Task<IRepository<Character>> CreateRepositoryAsync() => await _factory.CreateRepository<Character>()
            ?? throw new InvalidOperationException("Repository for Character not found.");

}
