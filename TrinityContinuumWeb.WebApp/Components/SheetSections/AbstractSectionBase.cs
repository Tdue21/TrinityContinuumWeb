using Microsoft.AspNetCore.Components;
using TrinityContinuum.Models;

namespace TrinityContinuum.WebApp.Components.SheetSections;

public class AbstractSectionBase : ComponentBase
{
    [Parameter] public Character Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Model != null)
        {
            await base.OnInitializedAsync();
        }
    }
}
