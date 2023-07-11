using WebAPI;
using WebAPI.Middlewares;
using WebAPI.Persistance;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddWebAPIServices(builder.Configuration)
    .AddControllers();

var app = builder.Build();

if (builder.Configuration.GetValue<bool>("SeedData"))
{
    var scopeFactory = app.Services.GetService<IServiceScopeFactory>();

    using var scope = scopeFactory.CreateScope();

    var discographySeeder = scope.ServiceProvider.GetService<DiscographySeeder>();
    discographySeeder.Seed();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
