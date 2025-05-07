using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using TrinityContinuum.Models;

namespace TrinityContinuum.WebApp.Clients;

public interface IApiClient
{
    Task<Character> GetCharacter(int id);
}

public class ApiClient(IHttpClientFactory clientFactory) : IApiClient
{
    private readonly IHttpClientFactory _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

    public async Task<Character> GetCharacter(int id)
    {
        var client = _clientFactory.CreateClient("API");
        var response = await client.GetAsync($"api/character/{id}");

        if(response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Character>(data)!;
        }
        else
        {
            throw new HttpRequestException("", null, response.StatusCode);
                
        }
    }
}
