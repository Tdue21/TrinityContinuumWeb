using TrinityContinuum.Server.Models;
using TrinityContinuum.Server.Services;
using TrinityContinuumWeb.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApplicationSettings>(builder.Configuration.GetSection(ApplicationSettings.SectionName));


// Add services to the container.
builder.Services.AddSingleton<IEnvironmentService, EnvironmentService>();
builder.Services.AddScoped<IDataProviderService, FileProviderService>();
builder.Services.AddScoped<ICharacterService, CharacterService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

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
