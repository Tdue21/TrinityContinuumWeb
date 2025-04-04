using Microsoft.AspNetCore.Components;
using TrinityContinuumWeb.Models;

namespace TrinityContinuum.WebApp.Components.SheetSections;
public partial class GeneralSectionBase : ComponentBase
{
    [Parameter] public Character Model { get; set; }
}

