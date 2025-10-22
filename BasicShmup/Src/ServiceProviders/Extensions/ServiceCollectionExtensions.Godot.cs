using System.Collections.Generic;
using BasicShmup.Domain.Entities.Ship;
using BasicShmup.Domain.Movements;
using BasicShmup.DomainAdaptors.Collision;
using BasicShmup.Entities.Ships;
using BasicShmup.ServiceProviders.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.ServiceProviders.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddGodotIntegrationServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddSingleton<ICollisionDetector, GodotCollisionDetector>()
            .AddSingleton(typeof(INodeReference<>), typeof(NodeReference<>))
            .AddSingleton<IShipFactory, ShipFactory>();
    }

    public static IServiceCollection AddConfigurations(
        this IServiceCollection serviceCollection,
        IEnumerable<IConfiguration> configurations)
    {
        foreach (var configuration in configurations)
            configuration.Configure(serviceCollection);

        return serviceCollection;
    }
}
