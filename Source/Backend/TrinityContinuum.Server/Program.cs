using System.IO.Abstractions;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using TrinityContinuum.Identity;
using TrinityContinuum.Models;
using TrinityContinuum.Server.Models;
using TrinityContinuum.Server.Services;
using TrinityContinuum.Services;
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


    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    // 1. Configure Entity Framework Core with SQLite
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));

    // 2. Configure Identity
    builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
    })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    // 3. Configure JWT Authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
        });


    builder.Services.AddRepositories();
    //builder.Services.AddAuthentication().AddJwtBearer();
    //builder.Services.AddAuthorizationBuilder();

    // Add services to the container.
    builder.Services.AddSingleton<IEnvironmentService, EnvironmentService>();
    builder.Services.AddScoped<IDataProviderService, FileProviderService>();
    builder.Services.AddScoped<ICharacterService, CharacterService>();
    builder.Services.AddScoped<IPowersService, PowersService>();
    builder.Services.AddScoped<IFileSystem, FileSystem>();

    builder.Services.AddControllers();
    builder.Services.AddProblemDetails();

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
    app.UseExceptionHandler();
    app.UseStatusCodePages();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "v1");
        });
    }

    app.UseHttpsRedirection();

    //app.UseAuthentication();
    //app.UseAuthorization();

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


namespace TrinityContinuum.Server
{
    public partial class Program
    {
        // This partial class is used to allow the Program class to be used in tests.
        // It can be extended with additional methods or properties as needed.
    }
}