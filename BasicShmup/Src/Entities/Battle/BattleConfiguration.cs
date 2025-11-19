using Godot;

namespace BasicShmup.Entities.Battle;

public class BattleConfiguration(Viewport mainViewport) : IBattleConfiguration
{
    public Vector2 BoundaryMinimum => mainViewport.GetVisibleRect().Position;
    public Vector2 BoundaryMaximum => mainViewport.GetVisibleRect().Size;
}
