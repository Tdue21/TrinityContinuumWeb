using Microsoft.Extensions.DependencyInjection;
using TrinityContinuum.Models.Entities;

namespace TrinityContinuum.Services.Repositories;

public static class RepositoryRegistrationExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Register the repository factory.
        services.AddScoped<IRepositoryFactory, RepositoryFactory>();

        // Register generic repositories.
        services.AddScoped(typeof(ISingleFileRepository<>), typeof(SingleFileRepository<>));
        services.AddScoped(typeof(IRepository<>), typeof(GenericFileRepository<>));

        // Register typed repositories.
        services.AddKeyedScoped<IRepository<Character>, CharacterRepository>(nameof(Character));

        return services;
    }
}