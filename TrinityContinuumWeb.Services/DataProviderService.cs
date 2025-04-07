using System.Text;

namespace TrinityContinuumWeb.Services;

/// <summary>
/// This interface describes the methods that a data provider service should implement.
/// </summary>
public interface IDataProviderService
{
    /// <summary>
    /// Read the data blob identified by <paramref name="id"/> in the catalog <paramref name="catalog"/>.
    /// </summary>
    /// <param name="catalog">Name of the catalog of the data blob</param>
    /// <param name="id">Id of the data blob</param>
    /// <param name="cancellationToken"></param>
    /// <returns>A string containing the file data</returns>
    Task<string> ReadData(string catalog, string id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Write the data blob <paramref name="content"/> identified by <paramref name="id"/> to the catalog <paramref name="catalog"/>.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task WriteData(string catalog, string id, string content, CancellationToken cancellationToken = default);

    /// <summary>
    /// Return a list of all blobs in the catalog defined.
    /// </summary>
    /// <param name="catalog">Name of the catalog of the data blob</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An <seealso cref="IEnumerable{T}"/> of strings containing the ids of all blobs in the catalog.</returns>
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
