using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TrinityContinuum.Models;
using TrinityContinuum.WebApp.Clients;

namespace TrinityContinuum.WebApp.Components.Pages;

public class SheetComponent : ComponentBase
{
    [Inject]
    public IApiClient ApiClient { get; set; }

    [Inject]
    public IHttpClientFactory HttpClientFactory { get; set; }

    [Parameter]
    public int CharacterId { get; set; } = 0;

    public Character Model { get; set; }


    protected override void OnInitialized()
    {
        if (CharacterId != 0)
        {
            var client = HttpClientFactory.CreateClient("API");
            var response = client.GetAsync($"api/character/{CharacterId}").Result;
            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                Model = JsonConvert.DeserializeObject<Character>(data)!;
            }
            else
            {
                // Handle error
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }



        //var data = ApiClient.GetCharacter(2);
        //data.Wait();
        //Model = data.Result;    
        base.OnInitialized();
    }
    protected override async Task OnInitializedAsync()
    {
        try
        {
            //Model = await ApiClient.GetCharacter(2);
            //StateHasChanged();

            //var client = HttpClientFactory.CreateClient("API");
            //var response = client.GetAsync("api/character/2").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = response.Content.ReadAsStringAsync().Result;
            //    Model = JsonConvert.DeserializeObject<Character>(data)!;
            //}
            //else
            //{
            //    // Handle error
            //    Console.WriteLine($"Error: {response.StatusCode}");
            //}

            //var data = File.ReadAllText(@"C:\Development\Projects\TrinityContinuumWeb\Data\1.json");
            //Model = JsonConvert.DeserializeObject<Character>(data)!;


        }
        catch (Exception)
        {
            throw;
        }
        await base.OnInitializedAsync();
    }

    /*
    private Character test = new Character
    {
        Name = "John Doe",
        Player = "Jane Doe",
        Concept = "Concept",
        PsiOrder = "Psi Order",
        OriginPath = new() { Name = "Origin Path", Dots = 1 },
        RolePath = new() { Name = "Role Path", Dots = 1 },
        SocietyPath = new() { Name = "Society Path", Dots = 1 },
        Skills =
        [
            new() { Name = "Aim", Dots = 0, Specialties = ["Pistols", "SMGs"], Tricks = ["Trick Shot"] },
            new() { Name = "Athletics", Dots = 0 },
            new() { Name = "Close Combat", Dots = 0 },
            new() { Name = "Command", Dots = 0 },
            new() { Name = "Culture", Dots = 0 },
            new() { Name = "Empathy", Dots = 0 },
            new() { Name = "Enigmas", Dots = 0 },
            new() { Name = "Humanities", Dots = 0 },
            new() { Name = "Integrity", Dots = 0 },
            new() { Name = "Larceny", Dots = 0 },
            new() { Name = "Medicine", Dots = 0 },
            new() { Name = "Persuasion", Dots = 0 },
            new() { Name = "Pilot", Dots = 0 },
            new() { Name = "Science", Dots = 0 },
            new() { Name = "Survival", Dots = 0 },
            new() { Name = "Technology", Dots = 0 },
        ],
        Attributes =
        {
            PreferredApproach = PreferredApproach.Finesse,
            Mental =
            {
                new() { Name = "Intellect", Dots = 2 },
                new() { Name = "Cunning", Dots = 3 },
                new() { Name = "Resolve", Dots = 3 },
            },
            Physical =
            {
                new() { Name = "Might", Dots = 3 },
                new() { Name = "Dexterity", Dots = 2 },
                new() { Name = "Stamina", Dots = 5 },
            },
            Social =
            {
                new() { Name = "Presence", Dots = 3 },
                new() { Name = "Manipulation", Dots = 2 },
                new() { Name = "Composure", Dots = 3 },
            },
        },
        Edges =
        {
            new() { Name = "Endurance", Dots = 1 },
        },
    };
    */
}
