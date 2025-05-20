using Microsoft.AspNetCore.Components;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Services;

namespace TrinityContinuum.WebApp.Components.Pages;

public class SheetComponent : ComponentBase
{
    [Inject]
    public required ICharacterService CharacterService { get; set; }

    [Inject]
    public required ToastService ToastService { get; set; } = null!;

    [Parameter]
    public int CharacterId { get; set; } = 0;

    public Character? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        try
        {
            if (CharacterId != 0)
            {
                Model = await CharacterService.GetCharacterAsync(CharacterId, CancellationToken.None);
                StateHasChanged();
            }
        }
        catch (Exception ex)
        {
            ToastService.ShowError($"Error loading character: {ex.Message}", 5);
            //throw;
        }
        await base.OnInitializedAsync();
    }
}
