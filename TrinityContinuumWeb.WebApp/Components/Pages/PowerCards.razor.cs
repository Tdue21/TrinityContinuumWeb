using Microsoft.AspNetCore.Components;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Services;

namespace TrinityContinuum.WebApp.Components.Pages;

public partial class PowerCardsBase : ComponentBase
{
    [Inject]
    public IPowersService PowersService { get; set; } = default!;
    [Inject]
    public ICharacterService CharacterService { get; set; } = default!;

    [Parameter]
    public int CharacterId { get; set; } = 0;
    public Character? Character { get; set; } = null;
    public IEnumerable<PsiPower>? Powers { get; private set; }

    public int PageCount { get; set; } = 9;
    public int PageTotal { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Character = await CharacterService.GetCharacterAsync(CharacterId, CancellationToken.None);
        if(Character == null)
        {
            throw new InvalidOperationException("Character not found.");
        }

        Powers = await PowersService.GetPowers(CancellationToken.None);
        if (Powers == null)
        {
            Powers = Enumerable.Empty<PsiPower>();
        }
        else
        {
            Powers = Powers.Where(x => Character.Psi.Aptitudes.Contains(x.Aptitude))
                           .DistinctBy(x => x.Name)
                           .OrderBy(x => x.Aptitude)
                           .ThenBy(x => x.Mode)
                           .ThenBy(x => x.Dots)
                           .ThenBy(x => x.Name);

            if (Character.Psi.Trait < 7)
            {
                Powers = Powers.Where(x => x.Dots < 7);
            }
            if (Character.Psi.Trait < 6)
            {
                Powers = Powers.Where(x => x.Dots < 6);
            }

            PageTotal = (int)Math.Ceiling((double)Powers.Count() / PageCount);
        }

        StateHasChanged();
    }

    protected IEnumerable<PsiPower> GetSelectedPowers(int pageIndex)
    {
        if(Character == null || Powers == null)
        {
            return Enumerable.Empty<PsiPower>();
        }

        var result = Powers.Skip(pageIndex * PageCount).Take(PageCount);

        return result;
    }
}
