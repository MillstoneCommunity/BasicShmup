using System.Collections.Generic;
using System.Linq;
using Godot;

namespace BasicShmup.Extensions;

public static class NodeExtensions
{
    public static IEnumerable<Node> GetNodesInSubTree(this Node root)
    {
        yield return root;

        foreach (var childNode in root.GetChildren())
        foreach (var childSubTreeNode in GetNodesInSubTree(childNode))
            yield return childSubTreeNode;
    }

    public static IEnumerable<T> GetChildren<T>(this Node node) where T : Node
    {
        return node
            .GetChildren()
            .OfType<T>();
    }
}
