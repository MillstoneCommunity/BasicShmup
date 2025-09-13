using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Configurations;

// Disable warning about Godot.Object derived class not being partial: it does not apply to abstract classes
#pragma warning disable GD0001
public abstract class ConfigurationResource : Resource
#pragma warning restore GD0001
{
    public abstract void Configure(IServiceCollection serviceCollection);
}
