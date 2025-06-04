using Microsoft.AspNetCore.Components;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Services;

namespace TrinityContinuum.WebApp.Components.Pages;
public partial class PowerCardsBase : ComponentBase
{
    [Inject]
    public IPowersService PowersService { get; set; } = default!;
    public IEnumerable<PsiPower>? Powers { get; private set; }
    public IEnumerable<string>? Aptitudes { get; private set; }
    public IEnumerable<string>? Modes { get; private set; }
    public string? SelectedAptitude { get; set; }
    public string? SelectedMode { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        Powers = await PowersService.GetPowers(CancellationToken.None);

        Aptitudes = Powers.Select(x => x.Aptitude)
                          .Distinct()
                          .OrderBy(x => x);

        StateHasChanged();
    }

    protected void SetSelectedAptitude(string? aptitude)
    {
        SelectedAptitude = aptitude;
        SelectedMode = null;
        Modes = Powers.Where(x => x.Aptitude == SelectedAptitude)
                          .Select(x => x.Mode)
                          .Distinct()
                          .OrderBy(x => x);

        StateHasChanged();
    }

    protected IEnumerable<PsiPower> GetSelectedPowers()
    {
        var result = Powers.Where(x => (string.IsNullOrEmpty(SelectedAptitude) || x.Aptitude == SelectedAptitude)
                                    && (string.IsNullOrEmpty(SelectedMode) || x.Mode == SelectedMode))
                           .DistinctBy(x => x.Name)
                           .OrderBy(x => x.Aptitude)
                           .ThenBy(x => x.Mode)
                           .ThenBy(x => x.Dots)
                           .ThenBy(x => x.Name);

        return result;
    }
}
