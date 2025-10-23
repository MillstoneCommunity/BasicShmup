using System.Collections.Generic;
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
}