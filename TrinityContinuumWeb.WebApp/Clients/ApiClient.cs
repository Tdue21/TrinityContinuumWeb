using Newtonsoft.Json;
using TrinityContinuum.Models;

namespace TrinityContinuum.WebApp.Clients;

/// <summary>
/// 
/// </summary>
public interface IApiClient
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<string> GetAsync(string uri, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Character?> GetCharacter(int id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<CharacterSummary>?> GetCharacters(CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<PsiPower>?> GetPowers(CancellationToken cancellationToken);
}

/// <summary>
/// 
/// </summary>
/// <param name="logger"></param>
/// <param name="clientFactory"></param>
public class ApiClient(ILogger<ApiClient> logger, IHttpClientFactory clientFactory) : IApiClient
{
    private readonly ILogger<ApiClient> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IHttpClientFactory _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));

    public async Task<string> GetAsync(string uri, CancellationToken cancellationToken)
    {
        var client = _clientFactory.CreateClient("API");
        var response = await client.GetAsync(uri, cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }
        else
        {
            throw new HttpRequestException("GET Action failed.", null, response.StatusCode);
        }
    }

    public async Task<Character?> GetCharacter(int id, CancellationToken cancellationToken)
    {
        try
        {
            var response = await GetAsync($"api/character/{id}", cancellationToken);
            var result = JsonConvert.DeserializeObject<Character>(response);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching character with ID {Id}", id);
            throw;
        }
    }

    public async Task<IEnumerable<CharacterSummary>?> GetCharacters(CancellationToken cancellationToken)
    {
        try
        {
            var response = await GetAsync($"api/character/list", cancellationToken);
            var result = JsonConvert.DeserializeObject<IEnumerable<CharacterSummary>>(response);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching character list.");
            throw;
        }
    }

    public async Task<IEnumerable<PsiPower>?> GetPowers(CancellationToken cancellationToken)
    {
        try
        {
            var response = await GetAsync($"api/powers/list", cancellationToken);
            var result = JsonConvert.DeserializeObject<IEnumerable<PsiPower>>(response);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching powers list.");
            throw;
        }
    }
}
