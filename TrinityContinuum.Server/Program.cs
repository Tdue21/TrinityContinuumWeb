using TrinityContinuum.Server.Models;
using TrinityContinuum.Server.Services;
using Serilog;
using TrinityContinuum.Services;
using TrinityContinuum.Models;
using TrinityContinuum.Services.Repositories;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", Serilog.Events.LogEventLevel.Debug, rollingInterval: RollingInterval.Day)
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSerilog(); 
    builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(ApplicationSettings.SectionName));


    builder.Services.AddRepositories();

    // Add services to the container.
    builder.Services.AddSingleton<IEnvironmentService, EnvironmentService>();
    builder.Services.AddScoped<IDataProviderService, FileProviderService>();
    builder.Services.AddScoped<ICharacterService, CharacterService>();
    builder.Services.AddScoped<IPowersService, PowersService>();

    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddHttpLogging(logging =>
    {
        logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.RequestProperties;
        logging.CombineLogs = true;
    });

    builder.WebHost.ConfigureKestrel((context, options) => 
    { 
        options.Configure(context.Configuration.GetSection("Kestrel"));
    });

    var app = builder.Build();
    app.UseHttpLogging();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "v1");
        });
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

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