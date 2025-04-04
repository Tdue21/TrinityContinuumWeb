using Microsoft.AspNetCore.Components;
using TrinityContinuumWeb.Models;

namespace TrinityContinuum.WebApp.Components.SheetSections;
public partial class PsiSectionBase : ComponentBase
{
    [Parameter]
    public Character Model { get; set; }

    protected int PsiPoints { get; set; }

    protected int MaxTolerance { get; set; }

    protected override Task OnInitializedAsync()
    {
        int[] points = [0, 1, 5, 10, 15, 20, 30, 40];
        PsiPoints = points[Model.Psi.Trait];

        MaxTolerance = Model.Attributes.Physical.First(x => x.Name == "Stamina").Dots +
                       Model.Psi.Trait;

        return base.OnInitializedAsync();
    }
}
