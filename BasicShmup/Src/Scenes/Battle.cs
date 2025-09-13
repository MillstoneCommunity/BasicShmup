using BasicShmup.Domain.GameStates;
using BasicShmup.Scenes.SceneConfigurations;
using Godot;

namespace BasicShmup.Scenes;

[NodeService]
public partial class Battle : Node2D
{
    [Inject]
    private readonly IBattleState _battleState = null!;

    public override void _Ready()
    {
        _battleState.Start();
    }
}
