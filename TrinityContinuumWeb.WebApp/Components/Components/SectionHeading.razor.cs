using Microsoft.AspNetCore.Components;

namespace TrinityContinuum.WebApp.Components.Components;
public partial class SectionHeadingBase : ComponentBase
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}
