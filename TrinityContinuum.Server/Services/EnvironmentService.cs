using Microsoft.Extensions.Options;
using TrinityContinuum.Server.Models;
using TrinityContinuum.Services;

namespace TrinityContinuum.Server.Services;

public class EnvironmentService(IWebHostEnvironment environment, IOptions<ApplicationSettings> settings) : IEnvironmentService
{
    private readonly IWebHostEnvironment _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    private readonly ApplicationSettings _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

    private string? _rootPath = null;

    public string RootPath
    {
        get
        {
            _rootPath ??= Path.Combine(_environment.ContentRootPath, _settings.DataFolder ?? "Data");
            return _rootPath;
        }
    }
}
