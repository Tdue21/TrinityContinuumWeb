namespace TrinityContinuumWeb.Services;

/// <summary>
/// This interface describes the methods that a data provider service should implement.
/// </summary>
public interface IDataProviderService
{
    /// <summary>
    /// Read the data for a file identified by <paramref name="id"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>A string containing the file data</returns>
    Task<string> ReadFile(string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read the data for all files.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>An <seealso cref="IEnumerable{T}"/> of strings containing the data of all files.</returns>
    Task<IEnumerable<string>> ReadFiles(CancellationToken cancellationToken = default);
}

/// <summary>
/// 
/// </summary>
public class FileProviderService(IEnvironmentService environment) : IDataProviderService
{
    private readonly IEnvironmentService _environment = environment ?? throw new ArgumentNullException(nameof(environment));

    public async Task<string> ReadFile(string id, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, $"{id}.json");
        path = Path.GetFullPath( path );
        return await File.ReadAllTextAsync(path, cancellationToken);
    }

    public async Task<IEnumerable<string>> ReadFiles(CancellationToken cancellationToken = default)
    {
        var files = Directory.EnumerateFiles(_environment.RootPath, "*.json", SearchOption.TopDirectoryOnly);
        var data = new List<Task<string>>();
        foreach(var file in files)
        {
            var id = int.Parse(Path.GetFileNameWithoutExtension(file));
            data.Add(File.ReadAllTextAsync(file, cancellationToken));
        }

        var result = await Task.WhenAll(data);
        return result;
    }
}
