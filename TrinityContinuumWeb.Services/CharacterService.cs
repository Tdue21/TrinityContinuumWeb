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
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="character"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> SaveCharacter(int id, Character character, CancellationToken cancellationToken);
}

/// <summary>
/// 
/// </summary>
public class CharacterService(IRepositoryFactory factory) : ICharacterService
{
    private readonly IRepositoryFactory _factory = factory ?? throw new ArgumentNullException(nameof(factory));

    public async Task<Character?> GetCharacterFromId(int id, CancellationToken cancellationToken = default)
    {
        var repository = await CreateRepositoryAsync(cancellationToken);
        return await repository.GetAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<CharacterSummary?>?> GetCharacterList(CancellationToken cancellationToken = default)
    {
        var repository = await CreateRepositoryAsync(cancellationToken);
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

    public async Task<bool> SaveCharacter(int id, Character character, CancellationToken cancellationToken)
    {
        var repository = await CreateRepositoryAsync(cancellationToken);
        if(id <= 0)
        {
            // New character
            await repository.AddAsync(character, cancellationToken);
        }
        else
        {
            // Update existing character
            var _ = await repository.GetAsync(id, cancellationToken) 
                ?? throw new KeyNotFoundException($"Character with ID {id} not found.");

            await repository.UpdateAsync(character, cancellationToken);
        }
        return true;
    }

    private async Task<IRepository<Character>> CreateRepositoryAsync(CancellationToken cancellationToken = default) 
        => await _factory.CreateRepository<Character>(cancellationToken)
            ?? throw new InvalidOperationException("Repository for Character not found.");

}
