using Microsoft.Extensions.DependencyInjection;
using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;

/// <summary>
/// 
/// </summary>
public interface IRepositoryFactory
{
    Task<IRepository<TEntity>> CreateRepository<TEntity>(CancellationToken cancellationToken = default) where TEntity : BaseEntity;
}

/// <summary>
/// 
/// </summary>
/// <param name="serviceProvider"></param>
public class RepositoryFactory(IServiceProvider serviceProvider) : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task<IRepository<TEntity>> CreateRepository<TEntity>(CancellationToken cancellationToken = default) where TEntity : BaseEntity
    {
        var repository = _serviceProvider.GetKeyedService<IRepository<TEntity>>(typeof(TEntity).Name);

        if (repository == null)
        {
            repository = _serviceProvider.GetRequiredService<IRepository<TEntity>>();
        }

        await repository.Initialize(cancellationToken);

        return repository;
    }
}
