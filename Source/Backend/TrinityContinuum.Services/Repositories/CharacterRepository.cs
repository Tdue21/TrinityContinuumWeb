using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;

public class CharacterRepository(IDataProviderService dataProvider) : GenericFileRepository<Character>(dataProvider)
{
    public override async Task<Character?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var character = await base.GetAsync(id, cancellationToken);
        return character;
    }

    internal async Task<byte[]?> GetImageAsync(int id, CancellationToken cancellationToken = default)
    {
        var character = await base.GetAsync(id, cancellationToken);
        if(character != null)
        {
            var token = !string.IsNullOrWhiteSpace(character.Token)
                ? await _dataProvider.ReadBinaryData(_catalogName, character.Token, cancellationToken)
                : null;
            return token;
        }
        return null;
    }
}


public static class CharacterRepositoryExtensions
{
    public static async Task<byte[]?> GetCharacterImage(this IRepository<Character> repository, int id, CancellationToken cancellationToken = default)
    {
        if (repository is not CharacterRepository characterRepository)
        {
            throw new InvalidOperationException("Repository is not a CharacterRepository.");
        }
        return await characterRepository.GetImageAsync(id, cancellationToken);
    }
}
