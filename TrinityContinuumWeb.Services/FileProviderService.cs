using System.Text;

namespace TrinityContinuum.Services;

/// <summary>
/// 
/// </summary>
public class FileProviderService(IEnvironmentService environment) : IDataProviderService
{
    private readonly IEnvironmentService _environment = environment ?? throw new ArgumentNullException(nameof(environment));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<string> ReadData(string catalog, string id, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, catalog, id);
        path = Path.GetFullPath( path );
        return await File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task WriteData(string catalog, string id, string content, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, catalog, id);
        path = Path.GetFullPath(path);
        await File.WriteAllTextAsync(path, content, Encoding.UTF8, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public void DeleteData(string catalog, string id)
    {
        var path = Path.Combine(_environment.RootPath, catalog, id);
        path = Path.GetFullPath(path);
        File.Delete(path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<string>> GetDataList(string catalog, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, catalog);
        var files = Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly).Select(x => Path.GetFileName(x)) ?? [];

        return Task.FromResult(files);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<byte[]> ReadBinaryData(string catalog, string fileName, CancellationToken cancellationToken = default)
    {
        var path = Path.Combine(_environment.RootPath, catalog, fileName);
        path = Path.GetFullPath(path);
        return await File.ReadAllBytesAsync(path, cancellationToken);
    }

}
