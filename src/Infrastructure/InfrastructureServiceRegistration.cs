using Ecommerce.Aplication.Models;
using Ecommerce.Aplication.Persistence;
using Ecommerce.Infrastructure.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.Infrastructure.Persistence;

public static class InfrastructureServiceRegistration
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                                IConfiguration configuration
    )
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        return services;
    }

}