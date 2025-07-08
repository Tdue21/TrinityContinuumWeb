using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Services;

namespace TrinityContinuum.WebApp.Components.Pages;
public partial class HomeBase : ComponentBase
{
    [Inject] public NavigationManager NavManager { get; set; } = default!;
    [Inject] public ICharacterService CharacterService { get; set; } = null!;

    protected int SelectedCharacterId { get; set; } = 0;
    protected List<CharacterSummary>? Characters { get; set; } = null;

    protected override async Task OnInitializedAsync()
    {
        Characters = await CharacterService.GetCharactersAsync(CancellationToken.None);
        await base.OnInitializedAsync();
        StateHasChanged();
    }

    protected void OpenCharacterSheet()
    {
        if (SelectedCharacterId == 0)
        {
            return;
        }

        NavManager.NavigateTo($"/sheet/{SelectedCharacterId}");
    }

    protected void OpenPowerCards()
    {
        if (SelectedCharacterId == 0)
        {
            return;
        }

        NavManager.NavigateTo($"/powercards/{SelectedCharacterId}");
    }

}
