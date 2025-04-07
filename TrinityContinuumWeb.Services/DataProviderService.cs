using System.Text;

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
    Task<string> ReadData(string catalog, string id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WriteData(string catalog, string id, string content, CancellationToken cancellationToken = default);

    /// <summary>
    /// Read the data for all files.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>An <seealso cref="IEnumerable{T}"/> of strings containing the data of all files.</returns>
    Task<IEnumerable<string>> GetDataList(string catalog, CancellationToken cancellationToken = default);
}

/// <summary>
/// 
/// </summary>
public class FileProviderService(IEnvironmentService environment) : IDataProviderService
{
    private readonly IEnvironmentService _environment = environment ?? throw new ArgumentNullException(nameof(environment));

    public async Task<string> ReadData(string catalog, string id, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, catalog, id);
        path = Path.GetFullPath( path );
        return await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
    }

    public async Task WriteData(string catalog, string id, string content, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, catalog, id);
        path = Path.GetFullPath(path);
        await File.WriteAllTextAsync(path, content, Encoding.UTF8, cancellationToken);
    }

    public Task<IEnumerable<string>> GetDataList(string catalog, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, catalog);
        var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Select(x => Path.GetFileName(x)) ?? [];

        return Task.FromResult(files);
    }
}
