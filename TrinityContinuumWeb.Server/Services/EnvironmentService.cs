using TrinityContinuumWeb.Services;

namespace TrinityContinuumWeb.Server.Services;

public class EnvironmentService(IWebHostEnvironment environment) : IEnvironmentService
{
    private readonly IWebHostEnvironment _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    private string? _rootPath = null;

    public string RootPath
    {
        get
        {
            _rootPath ??= Path.Combine(_environment.ContentRootPath, "data");
            return _rootPath;
        }
    }
}
