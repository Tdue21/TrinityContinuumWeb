using Newtonsoft.Json;
using System.Collections.Concurrent;
using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;

public class SingleFileRepository<TEntity>(IDataProviderService dataProvider) : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly IDataProviderService _dataProvider = dataProvider;

    protected readonly ConcurrentBag<TEntity> _entities = new();
    protected readonly string _catalogName = typeof(TEntity).Name;

    public virtual async Task Initialize(CancellationToken cancellationToken)
    {
        var data = await _dataProvider.ReadData("", $"{_catalogName}.json", cancellationToken);
        var result = JsonConvert.DeserializeObject<IEnumerable<TEntity>>(data);
        if (result == null)
        {
            throw new InvalidOperationException($"Failed to deserialize data for {_catalogName}.");
        }

        _entities.Clear();

        foreach (var entity in result)
        {
            _entities.Add(entity);
        }
    }

    public virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) 
        => Task.FromResult<IEnumerable<TEntity>>(_entities.ToList());

    public virtual Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public virtual Task DeleteAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public virtual Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public virtual Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();

    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
