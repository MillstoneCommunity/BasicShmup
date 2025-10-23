using BasicShmup.Extensions;
using BasicShmup.ServiceProviders.Extensions;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.ServiceProviders;

public partial class ServiceProviderNode : Node
{
    private ServiceProvider? _serviceProvider;
    private IServiceScope? _scope;

    public override void _EnterTree()
    {
        _serviceProvider = CreateServiceProvider();
        _scope = _serviceProvider.CreateScope();

        var sceneTree = GetTree();
        InjectServicesInto(sceneTree.Root);
        sceneTree.NodeAdded += InjectServicesInto;
    }

    public override void _ExitTree()
    {
        var sceneTree = GetTree();
        sceneTree.NodeAdded -= InjectServicesInto;
    }

    private ServiceProvider CreateServiceProvider()
    {
        return new ServiceCollection()
            .BuildServiceProvider(
#if TOOLS
                new ServiceProviderOptions
                {
                    ValidateScopes = true,
                    ValidateOnBuild = true
                }
#endif
            );
    }

    private void InjectServicesIntoSubtreeOf(Node root)
    {
        var subTree = root.GetNodesInSubTree();
        foreach (var node in subTree)
            InjectServicesInto(node);
    }

    private void InjectServicesInto(Node node)
    {
        if (_scope == null)
        {
            GD.PrintErr($"Cannot inject services into nodes when {nameof(_scope)} is null");
            return;
        }

        _scope.ServiceProvider.InjectServices(node);
    }

    protected override void Dispose(bool disposing)
    {
        _serviceProvider?.Dispose();
        base.Dispose(disposing);
    }
}
