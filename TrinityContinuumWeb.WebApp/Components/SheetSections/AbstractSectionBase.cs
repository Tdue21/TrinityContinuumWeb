using Microsoft.AspNetCore.Components;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Models;
using TrinityContinuum.WebApp.Services;

namespace TrinityContinuum.WebApp.Components.SheetSections;

public partial class AbstractSectionBase : ComponentBase
{
    [Inject] protected ToastService ToastService { get; set; } = null!;
    [Inject] protected ICharacterService CharacterService { get; set; } = null!;

    [Parameter] public Character? Model { get; set; }

    protected int Stamina { get; private set; }
    protected bool HasEndurance { get; private set; }
    protected Contact[] Contacts { get; private set; } = [];
    protected int PsiPoints { get; set; }
    protected int MaxTolerance { get; set; }
    protected IEnumerable<InjuryLevel> Levels { get; private set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        if (Model != null)
        {
            Contacts = [.. Model.OriginPath.Contacts,
                        .. Model.RolePath.Contacts,
                        .. Model.SocietyPath.Contacts];

            HasEndurance = Model.Edges.Any(x => x.Name == "Endurance");
            Stamina = Model.Attributes.Physical.TryGetValue("Stamina", out int result) ? result : 0;
            Levels = CharacterService.CalculateInjuryLevels(Stamina, HasEndurance);

            int[] psiPoints = [0, 1, 5, 10, 15, 20, 30, 40];
            PsiPoints = psiPoints[Model!.Psi.Trait];
            MaxTolerance = Stamina + Model.Psi.Trait;

            await base.OnInitializedAsync();
        }
    }
}
