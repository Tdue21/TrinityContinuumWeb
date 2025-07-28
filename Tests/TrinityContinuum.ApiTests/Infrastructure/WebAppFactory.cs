using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Runtime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NSubstitute;
using TrinityContinuum.Identity;
using TrinityContinuum.Server.Models;

namespace TrinityContinuum.ApiTests.Infrastructure;

public class  WebAppFactory : WebApplicationFactory<Server.Program>, IAsyncLifetime
{
    private Dictionary<string, MockFileData>? _files = null;
    private bool _allowAnonymous = false;
    private SqliteConnection _connection;

    public async Task InitializeAsync()
    {
        // Register a new in-memory SQLite database context
        _connection = new SqliteConnection("DataSource=:memory:");
        await _connection.OpenAsync();

    }

    async Task IAsyncLifetime.DisposeAsync()
    {
        await _connection.DisposeAsync();
    }


    public WebApplicationFactory<Server.Program> WithWebHostBuilder(
        Action<IWebHostBuilder> configuration, 
        Dictionary<string, MockFileData>? files = null, 
        bool allowAnonymous = false)
    {
        _files = files;
        _allowAnonymous = true; // allowAnonymous;

        return base.WithWebHostBuilder(configuration);
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the existing DbContext registration if it exists
            RemoveServiceDescriptor<ApplicationDbContext>(services);
            
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(_connection);
            });

            //if(_allowAnonymous)
            //{
            //    // Remove the default authorization policy provider.
            //    RemoveServiceDescriptor<IAuthorizationPolicyProvider>(services);

            //    // Add our fake provider which allows anonymous access to all policies.
            //    services.AddSingleton<IAuthorizationPolicyProvider, FakePolicyProvider>();
            //}

            // Build a temporary service provider to create the database
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
           
            var fs = new MockFileSystem(_files, scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>().ContentRootPath);
            services.AddSingleton<IFileSystem>(fs);

            //IOptions<ApplicationSettings> settings = Substitute.For<IOptions<ApplicationSettings>>();
            //settings.Value.Returns(new ApplicationSettings { DataFolder = "data", ApiKey = "your-api-key-here" });
            //services.AddSingleton<IOptions<ApplicationSettings>>(settings);

        });

        builder.UseEnvironment("Development");
    }

    protected override void ConfigureClient(HttpClient client)
    {
        var settings = Services.GetRequiredService<IOptions<ApplicationSettings>>().Value;
        client.DefaultRequestHeaders.Add("X-API-Key", settings.ApiKey);
    }

    private void RemoveServiceDescriptor<TService>(IServiceCollection services)
    {
        var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(TService));
        if (descriptor != null)
        {
            services.Remove(descriptor);
        }
    }
}
