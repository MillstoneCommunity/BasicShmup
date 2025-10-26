using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships;
using Godot;

namespace BasicShmup.Entities.Enemies;

public partial class EnemyController : Node
{
    public required Ship Ship { get; init; }

    public override void _PhysicsProcess(double delta)
    {
        Ship.Move(Direction.Left);
    }
}
