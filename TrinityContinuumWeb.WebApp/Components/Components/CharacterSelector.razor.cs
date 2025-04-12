using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using TrinityContinuumWeb.Models;

namespace TrinityContinuum.WebApp.Components.Components;
public partial class CharacterSelectorBase : ComponentBase
{
    [Inject] public IHttpClientFactory HttpClientFactory { get; set; } = null!;

    [Parameter] public int SelectedCharacter { get; set; } = 0;
    protected List<CharacterSummary> Characters { get; set; } = new();
    protected override async Task OnInitializedAsync()
    {
        var client = HttpClientFactory.CreateClient("API");
        var response = await client.GetAsync("character/list");
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<CharacterSummary>>(data)!;
            Characters = [.. list];
        }
        else
        {
            // Handle error
            Console.WriteLine($"Error: {response.StatusCode}");
        }

    }

}
