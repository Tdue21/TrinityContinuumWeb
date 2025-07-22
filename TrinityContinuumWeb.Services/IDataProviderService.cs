namespace TrinityContinuum.Services;

/// <summary>
/// This interface describes the methods that a data provider service should implement.
/// </summary>
public interface IDataProviderService
{
    /// <summary>
    /// Read the binary data blob identified by <paramref name="fileName"/> in the catalog <paramref name="category"/>
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<byte[]> ReadBinaryData(string catalog, string fileName, CancellationToken cancellationToken = default);

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
    /// Deletes the data blob identified by <paramref name="id"/> in the catalog <paramref name="catalog"/>.
    /// </summary>
    /// <param name="catalog"></param>
    /// <param name="id"></param>
    /// <param name="content"></param>
    /// <returns></returns>
    void DeleteData(string catalog, string id);

    /// <summary>
    /// Return a list of all blobs in the catalog defined.
    /// </summary>
    /// <param name="catalog">Name of the catalog of the data blob</param>
    /// <param name="cancellationToken"></param>
    /// <returns>An <seealso cref="IEnumerable{T}"/> of strings containing the ids of all blobs in the catalog.</returns>
    Task<IEnumerable<string>> GetDataList(string catalog, CancellationToken cancellationToken = default);
}
