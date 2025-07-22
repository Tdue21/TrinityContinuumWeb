using TrinityContinuum.Models;

namespace TrinityContinuum.Services.Repositories;

public class CharacterRepository(IDataProviderService dataProvider) : GenericFileRepository<Character>(dataProvider)
{
    public override async Task<Character?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var character = await base.GetAsync(id, cancellationToken);
        if(character != null)
        {
            var token = !string.IsNullOrWhiteSpace(character.Token)
                ? await _dataProvider.ReadBinaryData(_catalogName, character.Token, cancellationToken)
                : null;

            character.Token = (token != null)
                ? "data:image/webp;base64," + Convert.ToBase64String(token)
                : null;
        }

        return character;
    }
}