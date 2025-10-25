using System.Linq;
using BasicShmup.Dynamics;
using BasicShmup.Events;
using BasicShmup.Extensions;
using Godot;

namespace BasicShmup.Entities.Projectiles;

[GlobalClass]
public partial class Projectile : Node2D
{
    private readonly Speed _speed = 1400;
    public required IEntity Source { get; init; }
    public required Direction MovementDirection { get; init; }

    public override void _EnterTree()
    {
        var area = CreateCollisionArea();
        AddChild(area);

        var projectileController = new ProjectileController
        {
            MovementRoot = this,
            MovementDirection = MovementDirection,
            Speed = _speed
        };
        AddChild(projectileController);
    }

    private Node CreateCollisionArea()
    {
        var collisionShape = new CollisionShape2D
        {
            Shape = new CircleShape2D
            {
                Radius = 10
            },
            DebugColor = new Color(Colors.Red, .5f)
        };

        var area = new Area2D
        {
            Monitoring = true
        };
        area.AddChild(collisionShape);
        area.BodyEntered += HitBody;

        return area;
    }

    private void HitBody(Node2D hitBody)
    {
        var hitEntity = hitBody
            .GetChildren<EntityReference>()
            .FirstOrDefault()
            ?.Entity;

        if (hitEntity == Source)
            return;

        QueueFree();

        if (hitEntity is not IEventHandler<ProjectileHitEvent> eventHandler)
            return;

        eventHandler.Handle(new ProjectileHitEvent());
    }
}
