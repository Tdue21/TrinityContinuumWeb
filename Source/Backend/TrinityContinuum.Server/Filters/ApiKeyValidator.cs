using Microsoft.Extensions.Options;
using TrinityContinuum.Server.Models;

namespace TrinityContinuum.Server.Filters;

public interface IApiKeyValidator
{
    bool IsValid(string apiKey);
}

public class ApiKeyValidator(IOptions<ApplicationSettings> settings) : IApiKeyValidator
{
    private readonly ApplicationSettings _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    public bool IsValid(string apiKey)
    {
        return !string.IsNullOrEmpty(apiKey) && apiKey.Equals(_settings.ApiKey, StringComparison.OrdinalIgnoreCase);
    }
}
