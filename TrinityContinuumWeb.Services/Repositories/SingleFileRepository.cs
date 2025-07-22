using Newtonsoft.Json;
using System.Collections.Concurrent;
using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;

public class SingleFileRepository<TEntity>(IDataProviderService dataProvider) : ISingleFileRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IDataProviderService _dataProvider = dataProvider;

    protected readonly ConcurrentBag<TEntity> _entities = new();
    protected readonly string _catalogName = RepositoryUtilities.GetCatalogName<TEntity>();

    public virtual async Task Initialize(CancellationToken cancellationToken)
    {
        var data = await _dataProvider.ReadData("", $"{_catalogName}.json", cancellationToken);
        var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(data);
        if (result == null)
        {
            throw new InvalidOperationException($"Failed to deserialize data for {_catalogName}.json.");
        }

        _entities.Clear();

        foreach (var entity in result)
        {
            _entities.Add(entity);
        }
    }

    public virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IEnumerable<TEntity>>(_entities.ToList());
}
