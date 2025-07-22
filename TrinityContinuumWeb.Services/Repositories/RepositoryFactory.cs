using Microsoft.Extensions.DependencyInjection;
using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;

/// <summary>
/// 
/// </summary>
public interface IRepositoryFactory
{
    IRepository<TEntity> CreateRepository<TEntity>() where TEntity : BaseEntity;
}

/// <summary>
/// 
/// </summary>
/// <param name="serviceProvider"></param>
public class RepositoryFactory(IServiceProvider serviceProvider) : IRepositoryFactory
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : BaseEntity
    {
        var repository = _serviceProvider.GetRequiredService<IRepository<TEntity>>();
        repository.Initialize(CancellationToken.None).GetAwaiter();

        return repository;
    }
}
