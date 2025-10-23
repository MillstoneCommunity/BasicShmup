using System;
using System.Linq;
using System.Reflection;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.ServiceProviders.Extensions;

public static class ServiceProviderExtensions
{
    #region Inject services into nodes

    public static void InjectServices(this IServiceProvider serviceProvider, Node node)
    {
        var fields = node
            .GetType()
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(ShouldBeInjected);

        foreach (var field in fields)
            InjectService(serviceProvider, node, field);
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
