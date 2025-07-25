using System.IO.Abstractions;
using System.Text;

namespace TrinityContinuum.Services;

/// <summary>
/// 
/// </summary>
public class FileProviderService(IFileSystem fileSystem, IEnvironmentService environment) : IDataProviderService
{
    private readonly IFileSystem _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
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
        var path = _fileSystem.Path.Combine(_environment.RootPath, catalog, id);
        path = _fileSystem.Path.GetFullPath( path );
        return await _fileSystem.File.ReadAllTextAsync(path, Encoding.UTF8, cancellationToken);
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
        var path = _fileSystem.Path.Combine(_environment.RootPath, catalog, id);
        path = _fileSystem.Path.GetFullPath(path);
        
        var backupFile = $"{id}-{DateTimeOffset.UtcNow:yyyyMMddHHmmss}.bak";
        var backup = _fileSystem.Path.Combine(_environment.RootPath, catalog, backupFile);
        backup = _fileSystem.Path.GetFullPath(backup);

        _fileSystem.File.Copy(path, backup);

        await _fileSystem.File.WriteAllTextAsync(path, content, Encoding.UTF8, cancellationToken);
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
        var path = _fileSystem.Path.Combine(_environment.RootPath, catalog, id);
        path = _fileSystem.Path.GetFullPath(path);
        _fileSystem.File.Delete(path);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<string>> GetDataList(string catalog, CancellationToken cancellationToken = default)
    {
        var path = _fileSystem.Path.Combine(_environment.RootPath, catalog);
        var files = _fileSystem.Directory.EnumerateFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                                         .Select(x => _fileSystem.Path.GetFileName(x)) ?? [];

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
        var path = _fileSystem.Path.Combine(_environment.RootPath, catalog, fileName);
        path = _fileSystem.Path.GetFullPath(path);
        return await _fileSystem.File.ReadAllBytesAsync(path, cancellationToken);
    }

}
