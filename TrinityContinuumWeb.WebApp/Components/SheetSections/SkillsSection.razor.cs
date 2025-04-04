using Microsoft.AspNetCore.Components;
using TrinityContinuumWeb.Models;

namespace TrinityContinuum.WebApp.Components.SheetSections;

public partial class SkillsSectionBase : ComponentBase
{
    [Parameter] public Character Model { get; set; }
}
