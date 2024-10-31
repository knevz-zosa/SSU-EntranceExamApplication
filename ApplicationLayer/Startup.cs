using ApplicationLayer.Mapping;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ApplicationLayer;

public static class Startup
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        return services
            .AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(assembly);
            })
            .AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();

            }, AppDomain.CurrentDomain.GetAssemblies());
    }
}
