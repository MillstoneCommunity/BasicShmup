using Godot;

namespace BasicShmup.Scenes.SceneConfigurations;

public interface IScene
{
    Node[] NodeServices { get; }
}