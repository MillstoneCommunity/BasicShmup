using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.ServiceProviders.Configurations;

public interface IConfiguration
{
    void Configure(IServiceCollection serviceCollection);
}
