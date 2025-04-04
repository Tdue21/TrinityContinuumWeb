using Microsoft.AspNetCore.Components;
using TrinityContinuumWeb.Models;
using System.Linq;

namespace TrinityContinuum.WebApp.Components.SheetSections;
    
public partial class InjuryConditionsBase : ComponentBase
{
    [Parameter] public Character Model { get; set; }

    protected IEnumerable<(int Order, string Name, string Diff)> Levels { get; private set; }

    protected override Task OnInitializedAsync()
    {
        var levels = new List<(int Order, string Name, string Diff)> {
                (0, "Bruised", "+1"),
                (1, "Injured", "+2"),
                (2, "Maimed",  "+4")
        };

        var stamina = Model.Attributes.Physical.First(x => x.Name == "Stamina");

        if (stamina.Dots >= 3)
        {
            levels.Add((1, "Injured", "+2"));
        }

        if(stamina.Dots >= 5)
        {
            levels.Add((0, "Bruised", "+1"));
        }

        if (Model.Edges.Any(x => x.Name == "Endurance"))
        {
            levels.Add((0, "Bruised", "+1"));
        }
        Levels = levels.OrderBy(x => x.Order).ToList();

        return base.OnInitializedAsync();
    }
}
