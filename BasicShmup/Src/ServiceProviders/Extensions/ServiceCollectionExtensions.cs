using System.Collections.Generic;
using BasicShmup.Entities.Battle;
using BasicShmup.Events;
using BasicShmup.ServiceProviders.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.ServiceProviders.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurations(
        this IServiceCollection serviceCollection,
        IEnumerable<IConfiguration> configurations)
    {
        serviceCollection.AddNonResourceConfigurations();

        foreach (var configuration in configurations)
            configuration.Register(serviceCollection);

        return serviceCollection;
    }

    private static IServiceCollection AddNonResourceConfigurations(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IBattleConfiguration>(new BattleConfiguration());
    }

    public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddEventBroker();
    }

    private static IServiceCollection AddEventBroker(this IServiceCollection serviceCollection)
    {
        var eventBroker = new EventBroker();

        return serviceCollection
            .AddSingleton<IEventSender>(eventBroker)
            .AddSingleton<IEventReceiver>(eventBroker);
    }
}
