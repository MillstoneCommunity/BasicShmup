using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Battle;

public partial class BattleBounds : StaticBody2D
{
    private const string TopPlayerBoundary = nameof(TopPlayerBoundary);
    private const string LeftPlayerBoundary = nameof(LeftPlayerBoundary);
    private const string BottomPlayerBoundary = nameof(BottomPlayerBoundary);
    private const string RightPlayerBoundary = nameof(RightPlayerBoundary);

    [Inject]
    private readonly IBattleConfiguration _battleConfiguration = null!;

    public BattleBounds()
    {
        Name = nameof(BattleBounds);
    }

    public override void _Ready()
    {
        AddPlayerBounds();
    }

    private void AddPlayerBounds()
    {
        var boundaryMinimum = _battleConfiguration.BoundaryMinimum;
        var boundaryMaximum = _battleConfiguration.BoundaryMaximum;

        AddBoundingCollider(TopPlayerBoundary, boundaryMinimum, Vector2.Down);
        AddBoundingCollider(LeftPlayerBoundary, boundaryMinimum, Vector2.Right);
        AddBoundingCollider(BottomPlayerBoundary, boundaryMaximum, Vector2.Up);
        AddBoundingCollider(RightPlayerBoundary, boundaryMaximum, Vector2.Left);
    }

    private void AddBoundingCollider(
        string boundaryName,
        Vector2 position,
        Vector2 boundaryNormal)
    {
        var collider = new CollisionShape2D
        {
            Name = boundaryName,
            Position = position,
            Shape = new WorldBoundaryShape2D
            {
                Normal = boundaryNormal
            }
        };

        AddChild(collider);
    }
}
