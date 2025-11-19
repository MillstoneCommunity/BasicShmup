using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Battle;

public partial class BattleBounds : StaticBody2D
{
    [Inject]
    private readonly IBattleConfiguration _battleConfiguration = null!;

    public override void _Ready()
    {
        AddPlayerBounds();
    }

    private void AddPlayerBounds()
    {
        var boundaryMinimum = _battleConfiguration.BoundaryMinimum;
        var boundaryMaximum = _battleConfiguration.BoundaryMaximum;

        AddBoundingCollider(boundaryMinimum, Vector2.Down);
        AddBoundingCollider(boundaryMinimum, Vector2.Right);
        AddBoundingCollider(boundaryMaximum, Vector2.Up);
        AddBoundingCollider(boundaryMaximum, Vector2.Left);
    }

    private void AddBoundingCollider(Vector2 position, Vector2 boundaryNormal)
    {
        var collider = new CollisionShape2D
        {
            Position = position,
            Shape = new WorldBoundaryShape2D
            {
                Normal = boundaryNormal
            }
        };

        AddChild(collider);
    }
}
