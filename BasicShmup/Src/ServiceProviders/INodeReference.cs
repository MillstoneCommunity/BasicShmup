using Godot;

namespace BasicShmup.ServiceProviders;

public interface INodeReference<TNode> where TNode : Node
{
    TNode GetNode();
    void SetNode(TNode node);
}
