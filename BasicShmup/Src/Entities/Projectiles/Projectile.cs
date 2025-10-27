using System.Linq;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships.Controllers;
using BasicShmup.Events;
using BasicShmup.Extensions;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Projectiles;

[GlobalClass]
public partial class Projectile : Node2D
{
    [Inject]
    private readonly IProjectileConfiguration _projectileConfiguration = null!;

    public required IController SourceController { get; init; }
    public required Direction MovementDirection { get; init; }

    public override void _Ready()
    {
        var area = CreateCollisionArea();
        AddChild(area);

        var sprite = new Sprite2D
        {
            TextureFilter = TextureFilterEnum.Nearest,
            Texture = _projectileConfiguration.Texture,
            Scale = _projectileConfiguration.TextureScaling.AsUniformVector()
        };
        AddChild(sprite);

        var projectileController = new ProjectileController
        {
            MovementRoot = this,
            MovementDirection = MovementDirection,
            Speed = _projectileConfiguration.Speed
        };
        AddChild(projectileController);
    }

    private Node CreateCollisionArea()
    {
        var collisionShape = new CollisionShape2D
        {
            Shape = new CircleShape2D
            {
                Radius = _projectileConfiguration.ColliderRadius
            }
        };

        var area = new Area2D
        {
            Monitoring = true
        };
        area.AddChild(collisionShape);
        area.AreaEntered += CollideWith;

        return area;
    }

    private void CollideWith(Node2D hitNode)
    {
        var hitController = hitNode
            .GetChildren<ControllerReference>()
            .FirstOrDefault()
            ?.Controller;

        if (hitController == SourceController)
            return;

        QueueFree();

        if (hitController is not IEventHandler<ProjectileCollisionEvent> eventHandler)
            return;

        var damage = _projectileConfiguration.Damage;
        eventHandler.Handle(new ProjectileCollisionEvent(damage));
    }
}
