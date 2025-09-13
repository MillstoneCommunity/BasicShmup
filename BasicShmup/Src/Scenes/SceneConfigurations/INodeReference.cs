using Godot;

namespace BasicShmup.Scenes.SceneConfigurations;

public interface INodeReference<TNode> where TNode : Node
{
    TNode GetNode();
    void SetNode(TNode node);
}
