using BasicShmup.Configurations;
using BasicShmup.Domain.Entities.Ship;
using BasicShmup.Domain.Movements;
using BasicShmup.DomainAdaptors.Collision;
using BasicShmup.Entities.Ship;
using BasicShmup.Scenes.SceneConfigurations;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddGodotLayerServices(
        this IServiceCollection serviceCollection,
        ConfigurationResource[] configurationResources)
    {
        foreach (var configurationResource in configurationResources)
            configurationResource.Configure(serviceCollection);

        return serviceCollection
            .AddSingleton<ICollisionDetector, GodotCollisionDetector>()
            .AddSingleton(typeof(INodeReference<>), typeof(NodeReference<>))
            .AddSingleton<IShipFactory, RootShipFactory>();
    }
}
