using System;
using Godot;

namespace BasicShmup.ServiceProviders;

public class NodeReference<TNode> : INodeReference<TNode> where TNode : Node
{
    private TNode? _node;

    public TNode GetNode()
    {
        if (_node == null)
            throw new InvalidOperationException($"No {typeof(TNode)} node has been registered");

        return _node;
    }

    public void SetNode(TNode node)
    {
        if (_node != null)
            throw new InvalidOperationException($"Cannot set {typeof(TNode)} node, when that type of node has already been registered");

        _node = node;
    }
}
