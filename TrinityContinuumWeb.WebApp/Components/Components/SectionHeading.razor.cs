using Microsoft.AspNetCore.Components;

namespace TrinityContinuum.WebApp.Components.Components;
public partial class SectionHeadingBase : ComponentBase
{
    [Parameter] public string Heading { get; set; }
}
