using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using TrinityContinuum.Models.Dtos;
using TrinityContinuum.WebApp.Services;

namespace TrinityContinuum.WebApp.Components.Shared;
public partial class CharacterSelectorBase : ComponentBase
{
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ICharacterService CharacterService { get; set; } = null!;

    [SupplyParameterFromForm] public int SelectedCharacter { get; set; } = 0;

    protected List<CharacterSummary> Characters { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        Characters = await CharacterService.GetCharactersAsync(CancellationToken.None);
    }

    public void HandleValidSubmit(EditContext args)
    {
        if(SelectedCharacter == 0)
        {
            return;
        }
        NavigationManager.NavigateTo($"/sheet/{SelectedCharacter}");
    }

}
