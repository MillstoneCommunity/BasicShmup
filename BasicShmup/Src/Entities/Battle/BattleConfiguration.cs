using Godot;

namespace BasicShmup.Entities.Battle;

public class BattleConfiguration : IBattleConfiguration
{
    public Vector2 BoundaryMinimum => Vector2.Zero;
    public Vector2 BoundaryMaximum => DisplayServer.WindowGetSize();
}
