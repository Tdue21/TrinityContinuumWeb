using Microsoft.AspNetCore.Components;
using TrinityContinuumWeb.Models;
using System.Linq;

namespace TrinityContinuum.WebApp.Components.SheetSections;

public partial class InjuryConditionsBase : AbstractSectionBase
{

    protected IEnumerable<(int Order, string Name, string Diff)> Levels { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        if(Model == null)
        {
            return;
        }   

        await base.OnInitializedAsync();
        try
        {
            var levels = new List<(int Order, string Name, string Diff)> {
                (0, "Bruised", "+1"),
                (1, "Injured", "+2"),
                (2, "Maimed",  "+4")
            };

            var stamina = Model.Attributes.Physical.TryGetValue("Stamina", out int result) ? result : 0;

            if (stamina >= 3)
            {
                levels.Add((1, "Injured", "+2"));
            }

            if (stamina >= 5)
            {
                levels.Add((0, "Bruised", "+1"));
            }

            if (Model.Edges.Any(x => x.Name == "Endurance"))
            {
                levels.Add((0, "Bruised", "+1"));
            }
            Levels = levels.OrderBy(x => x.Order).ToList();

        }
        catch (Exception)
        {
            throw;
        }
    }
}
