using System.Linq;
using BasicShmup.Events;
using BasicShmup.Extensions;
using Godot;

namespace BasicShmup.Entities.Projectiles;

[GlobalClass]
public partial class Projectile : Node2D
{
    public Projectile()
    {
        var area = CreateCollisionArea();
        AddChild(area);
    }

    private Node CreateCollisionArea()
    {
        var collisionShape = new CollisionShape2D
        {
            Shape = new CircleShape2D
            {
                Radius = 50
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
        QueueFree();

        var hitEntity = hitBody
            .GetChildren<EntityReference>()
            .FirstOrDefault()
            ?.Entity;

        if (hitEntity is not IEventHandler<ProjectileHitEvent> eventHandler)
            return;

        eventHandler.Handle(new ProjectileHitEvent());
    }
}
