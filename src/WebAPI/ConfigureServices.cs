using Microsoft.EntityFrameworkCore;
using WebAPI.Interfaces;
using WebAPI.Persistance;
using WebAPI.Repositories;

namespace WebAPI;

public static class ConfigureServices
{
    public static IServiceCollection AddWebAPIServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DiscographyDbContext>(options => options.UseSqlite(configuration.GetConnectionString("DiscographyConnection")));

        services.AddTransient<DiscographySeeder>();

        services.AddScoped<IAlbumRepository, AlbumRepository>();
        services.AddScoped<IAlbumTypeRepository, AlbumTypeRepository>();
        services.AddScoped<IBandRepository, BandRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();

        return services;
    }
}
