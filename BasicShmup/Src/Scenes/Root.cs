using System.ComponentModel.DataAnnotations;
using BasicShmup.Configurations;
using BasicShmup.Extensions;
using BasicShmup.Validations;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Scenes;

public partial class Root : Node
{
    [ExportCategory("Configuration")]
    [ExportGroup("Scenes")]

    [Export]
    private PackedScene[] _globalScenes = [];

    [Export]
    [Required]
    private PackedScene _initialScene = null!;

    [ExportGroup("Other")]

    [Export]
    private ConfigurationResource[] _configurationResources = [];

    private ServiceProvider _services = null!;
    private IServiceScope _scope = null!;

    public override void _EnterTree()
    {
        CreateServiceProvider();

#if TOOLS
        new ResourceValidator().Run();
#endif

        foreach (var globalScene in _globalScenes)
            AddScene(globalScene);

        AddScene(_initialScene);
    }

    private void CreateServiceProvider()
    {
        _services = new ServiceCollection()
            .AddGodotLayerServices(_configurationResources)
            .AddDomain()
            .BuildServiceProvider(
#if TOOLS
                new ServiceProviderOptions
                {
                    ValidateScopes = true,
                    ValidateOnBuild = true
                }
#endif
            );

        _scope = _services.CreateScope();
    }


    private void AddScene(PackedScene packedScene)
    {
        var scene = packedScene.Instantiate();
        _scope.ServiceProvider.RegisterNodeServices(scene);
        _scope.ServiceProvider.InjectServices(scene);

        AddChild(scene);
    }

    protected override void Dispose(bool disposing)
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        _scope?.Dispose();
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        _services?.Dispose();

        base.Dispose(disposing);
    }
}
