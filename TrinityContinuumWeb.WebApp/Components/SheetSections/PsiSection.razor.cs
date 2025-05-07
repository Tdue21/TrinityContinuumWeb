namespace TrinityContinuum.WebApp.Components.SheetSections;

public partial class PsiSectionBase : AbstractSectionBase
{

    protected int PsiPoints { get; set; }

    protected int MaxTolerance { get; set; }

    protected override Task OnInitializedAsync()
    {
        if (Model == null)
        {
            return Task.CompletedTask;
        }

        int[] points = [0, 1, 5, 10, 15, 20, 30, 40];
        PsiPoints = points[Model.Psi.Trait];

        MaxTolerance = (Model.Attributes.Physical.TryGetValue("Stamina", out int result) ? result : 0) +
                       Model.Psi.Trait;

        return base.OnInitializedAsync();
    }
}
