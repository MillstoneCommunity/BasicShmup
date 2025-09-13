
using System;
using System.Linq;
using System.Reflection;
using BasicShmup.Scenes.SceneConfigurations;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Extensions;

public static class ServiceProviderExtensions
{
    #region Register node services

    public static void RegisterNodeServices(this IServiceProvider serviceProvider, Node root)
    {
        foreach (var node in root.GetNodesInSubTree())
        {
            if (!IsNodeService(node))
                continue;

            serviceProvider.RegisterNodeService(node);
        }
    }

    private static bool IsNodeService(Node node)
    {
        return node.GetType().IsDefined(typeof(NodeServiceAttribute));
    }

    private static void RegisterNodeService(this IServiceProvider serviceProvider, Node nodeService)
    {
        var genericRegisterMethod = typeof(ServiceProviderExtensions)
            .GetMethod(nameof(Register), BindingFlags.Static | BindingFlags.NonPublic)!;

        var nodeServiceType = nodeService.GetType();
        var registerMethod = genericRegisterMethod.MakeGenericMethod(nodeServiceType);
        registerMethod.Invoke(null, [serviceProvider, nodeService]);
    }

    private static void Register<TNode>(IServiceProvider serviceProvider, TNode nodeService) where TNode : Node
    {
        serviceProvider
            .GetRequiredService<INodeReference<TNode>>()
            .SetNode(nodeService);
    }

    #endregion

    #region Inject services into nodes

    public static void InjectServices(this IServiceProvider serviceProvider, Node root)
    {
        foreach (var node in root.GetNodesInSubTree())
        {
            var fields = node
                .GetType()
                .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(ShouldBeInjected);

            foreach (var field in fields)
                InjectService(serviceProvider, node, field);
        }
    }

    private static bool ShouldBeInjected(FieldInfo field)
    {
        return field.IsDefined(typeof(InjectAttribute));
    }

    private static void InjectService(IServiceProvider serviceProvider, Node node, FieldInfo field)
    {
        var fieldType = field.FieldType;

        object? fieldValue;
        if (fieldType.IsArray)
        {
            var elementType = fieldType.GetElementType()!;
            fieldValue = serviceProvider.GetServices(elementType);
        }
        else
        {
            fieldValue = IsNullable(fieldType)
                ? serviceProvider.GetService(fieldType)
                : serviceProvider.GetRequiredService(fieldType);
        }

        field.SetValue(node, fieldValue);
    }

    private static bool IsNullable(Type type)
    {
        return Nullable.GetUnderlyingType(type) != null;
    }

    #endregion
}
