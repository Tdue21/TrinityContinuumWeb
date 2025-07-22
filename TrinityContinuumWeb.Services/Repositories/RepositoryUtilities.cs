using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace TrinityContinuum.Services.Repositories;

public class RepositoryUtilities
{
    /// <summary>
    /// Retrieve the catalog name for a given entity type. Either uses the name specified in 
    /// the TableAttribute or defaults to the entity's class name.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static string GetCatalogName<TEntity>() where TEntity : class
    {
        var tableAttribute = typeof(TEntity).GetCustomAttribute<TableAttribute>();

        return tableAttribute != null 
            ? tableAttribute.Name.ToLowerInvariant()
            : typeof(TEntity).Name.ToLowerInvariant();
    }
}
