using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;
public interface ISingleFileRepository<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);
    Task Initialize(CancellationToken cancellationToken);
}