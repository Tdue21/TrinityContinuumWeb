using Microsoft.Extensions.DependencyInjection;
using TrinityContinuum.Models;

namespace TrinityContinuum.Services.Repositories;

public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Register the repository factory.
        services.AddSingleton<IRepositoryFactory, RepositoryFactory>();
        services.AddScoped(typeof(IRepository<>), typeof(GenericFileRepository<>));
        services.AddScoped<IRepository<Character>, CharacterRepository>();

        return services;
    }
}