using BasicShmup.Dynamics;
using BasicShmup.Entities.Battle;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Entities.Ships.States;
using BasicShmup.Events;
using BasicShmup.Extensions;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Ships;

public partial class Ship : Node2D, IShip
{
    [Inject]
    private readonly IEventSender _eventSender = null!;

    [Inject]
    private readonly IShipConfiguration _shipConfiguration = null!;

    private readonly Area2D _colliderArea = new();

    private IShipState _state = null!;

    public required IEntity RootEntity { get; init; }

    public new Position Position
    {
        get => base.Position;
        set => base.Position = value.VectorValue;
    }

    public bool IsDead => _state.IsDead;

    public override void _Ready()
    {
        var shipState = new ShipState
        {
            Health = _shipConfiguration.Health
        };
        _state = shipState;
        AddChild(shipState);

        var colliderShape = new CircleShape2D
        {
            Radius = _shipConfiguration.ColliderRadius
        };
        var collisionShape = new CollisionShape2D { Shape = colliderShape };
        _colliderArea.AddChild(collisionShape);

        var entityReference = new EntityReference { Entity = RootEntity };
        _colliderArea.AddChild(entityReference);

        var sprite = new Sprite2D
        {
            Texture = _shipConfiguration.Texture,
            Scale = _shipConfiguration.TextureScaling.AsUniformVector(),
            TextureFilter = CanvasItem.TextureFilterEnum.Nearest
        };
        _colliderArea.AddChild(sprite);

        AddChild(_colliderArea);
    }

    #region IShip

    public void FireProjectile()
    {
        if (!_state.CanFire)
            return;

        _state.SetFireCooldown();

        var projectile = new Projectile
        {
            Source = RootEntity,
            Position = _colliderArea.GlobalPosition,
            MovementDirection = Direction.Right
        };

        _eventSender.Send(new SpawnBattleNodeEvent(projectile));
    }

    public void TakeDamage(Damage damage)
    {
        _state.TakeDamage(damage);
    }

    #endregion
}
