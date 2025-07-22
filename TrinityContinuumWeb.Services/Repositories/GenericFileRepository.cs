using System.Collections.Concurrent;
using Newtonsoft.Json;
using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;

public class GenericFileRepository<TEntity>(IDataProviderService dataProvider) : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly IDataProviderService _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));

    protected readonly ConcurrentDictionary<int, TEntity> _entities = new();
    protected readonly string _catalogName = RepositoryUtilities.GetCatalogName<TEntity>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public virtual Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero.");
        }

        return _entities.TryGetValue(id, out var entity)
            ? Task.FromResult<TEntity?>(entity)
            : Task.FromResult<TEntity?>(null);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => Task.FromResult<IEnumerable<TEntity>>([.. _entities.Values]);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public virtual async Task<int> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        // Check if the entity is null and throw an exception if it is.
        ArgumentNullException.ThrowIfNull(entity);

        if (entity.Id == 0)
        {
            // If the entity Id is zero, assign a new Id.
            entity.Id = !_entities.IsEmpty ? _entities.Keys.Max() + 1 : 1;
        }

        if (_entities.ContainsKey(entity.Id))
        {
            throw new InvalidOperationException($"Entity with Id {entity.Id} already exists.");
        }
        _entities.TryAdd(entity.Id, entity);

        // Serialize the entity to JSON and write it to the data provider.
        var entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
        await _dataProvider
            .WriteData(_catalogName, $"{entity.Id}.json", entityJson, cancellationToken)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    throw task.Exception ?? new Exception("An error occurred while writing data.");
                }
            }, cancellationToken);

        return entity.Id;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        if (entity.Id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(entity), "Entity Id must be greater than zero.");
        }
        if (!_entities.ContainsKey(entity.Id))
        {
            throw new InvalidOperationException($"Entity with Id {entity.Id} does not exist.");
        }
        _entities[entity.Id] = entity; // Update the entity in the dictionary
        var entityJson = JsonConvert.SerializeObject(entity, Formatting.Indented);
        return _dataProvider
            .WriteData(_catalogName, $"{entity.Id}.json", entityJson, cancellationToken)
            .ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    throw task.Exception ?? new Exception("An error occurred while writing data.");
                }
            }, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Entity Id must be greater than zero.");
        }

        if (!_entities.TryRemove(id, out _))
        {
            throw new InvalidOperationException($"Entity with Id {id} does not exist.");
        }

        _dataProvider.DeleteData(_catalogName, $"{id}.json");

        return Task.CompletedTask;
    }

    /// <summary>
    /// Deletes the specified entity asynchronously.
    /// </summary>
    /// <param name="entity">The entity to delete. Cannot be <see langword="null"/>.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests. Defaults to <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entity);
        return DeleteAsync(entity.Id, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public virtual Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        if (id <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "Entity Id must be greater than zero.");
        }

        // Return true if the entity with the specified id exists, otherwise false.
        return Task.FromResult(_entities.ContainsKey(id));
    }

    public virtual Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        // Return true if the entity with the specified id exists, otherwise false.
        return Task.FromResult(_entities.Keys.Count);
    }

    public virtual async Task Initialize(CancellationToken cancellationToken)
    {
        var dataList = await _dataProvider.GetDataList(_catalogName, cancellationToken);

        foreach (var item in dataList)
        {
            if (item.EndsWith(".json"))
            {
                var entityData = await _dataProvider.ReadData(_catalogName, item, cancellationToken);
                var entity = JsonConvert.DeserializeObject<TEntity>(entityData);
                if (entity != null)
                {
                    if (!_entities.TryAdd(entity.Id, entity))
                    {
                        _entities[entity.Id] = entity; // Update if already exists
                    }
                }
            }
        }
    }
}
