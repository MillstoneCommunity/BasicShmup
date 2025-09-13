using BasicShmup.Domain.Entities;
using BasicShmup.Domain.GameStates;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Extensions;

public static partial class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddState<BattleState>()
            .AddSingleton<IEntityRepository, EntityRepository>();
    }

    private static IServiceCollection AddState<TState>(this IServiceCollection serviceCollection)
        where TState : class, IBattleState
    {
        return serviceCollection.AddScoped<IBattleState, TState>();
    }
}
