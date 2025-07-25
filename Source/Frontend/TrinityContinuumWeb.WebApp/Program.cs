using Microsoft.Extensions.Options;
using TrinityContinuum.WebApp.Clients;
using TrinityContinuum.WebApp.Components;
using TrinityContinuum.WebApp.Models;
using Serilog;
using TrinityContinuum.WebApp.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", Serilog.Events.LogEventLevel.Debug, rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog();

    // Add services to the container.
    builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(ApplicationSettings.SectionName));
    builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

    builder.Services.AddHttpClient("API", (provider, config) =>
    {
        var configuration = provider.GetRequiredService<IOptions<ApplicationSettings>>();
        if (configuration?.Value?.ApiBaseUrl is null)
        {
            throw new ArgumentNullException(nameof(configuration.Value.ApiBaseUrl), "API base URL is not configured.");
        }
        config.BaseAddress = new Uri(configuration.Value.ApiBaseUrl);
    });

    builder.Services.AddScoped<IApiClient, ApiClient>();
    builder.Services.AddScoped<ICharacterService, CharacterService>();
    builder.Services.AddScoped<IPowersService, PowersService>();
    builder.Services.AddScoped<IMarkdownRenderService, MarkdownRenderService>();
    builder.Services.AddSingleton<ToastService>();

    builder.WebHost.ConfigureKestrel((context, options) =>
    {
        options.Configure(context.Configuration.GetSection("Kestrel"));
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseAntiforgery();
    app.MapStaticAssets();
    app.MapRazorComponents<App>()
       .AddInteractiveServerRenderMode();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}