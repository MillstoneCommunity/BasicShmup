using Godot;

namespace BasicShmup.Entities.Battle;

public interface IBattleConfiguration
{
    Vector2 BoundaryMinimum { get; }
    Vector2 BoundaryMaximum { get; }
}
