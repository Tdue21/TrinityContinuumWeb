using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TrinityContinuum.Services;

namespace TrinityContinuum.ApiTests;

public class  WebAppFactory : WebApplicationFactory<Server.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var environment = Substitute.For<IWebHostEnvironment>();
            environment.ContentRootPath.Returns("C:\\WebData");

            services.AddSingleton(environment);
        });

        builder.UseEnvironment("Development");
    }
}
