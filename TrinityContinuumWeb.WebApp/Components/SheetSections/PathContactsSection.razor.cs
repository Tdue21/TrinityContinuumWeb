using TrinityContinuum.Models;

namespace TrinityContinuum.WebApp.Components.SheetSections;

public partial class PathContactsSectionBase : AbstractSectionBase
{
    protected Contact[]? Contacts { get; set; }

    protected override async Task OnInitializedAsync()
    {
        if (Model != null)
        {
            await base.OnInitializedAsync();

            Contacts = Model.OriginPath.Contacts
                .Concat(Model.RolePath.Contacts)
                .Concat(Model.SocietyPath.Contacts)
                .ToArray();
        }
    }

}
