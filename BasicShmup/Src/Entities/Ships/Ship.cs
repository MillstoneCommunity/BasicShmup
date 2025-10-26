using BasicShmup.Dynamics;
using BasicShmup.Entities.Battle;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Events;
using BasicShmup.Extensions;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Ships;

public partial class Ship : Node, IShip
{
    [Inject]
    private readonly IEventSender _eventSender = null!;

    [Inject]
    private readonly IShipConfiguration _shipConfiguration = null!;

    private readonly IShipState _state;

    private readonly CharacterBody2D _body = new()
    {
        MotionMode = CharacterBody2D.MotionModeEnum.Floating
    };

    public required IEntity RootEntity { get; init; }

    public Vector2 Position
    {
        get => _body.Position;
        set => _body.Position = value;
    }

    public bool IsDead => _state.IsDead;

    public Ship()
    {
        var shipState = new ShipState();
        _state = shipState;
        AddChild(shipState);
    }

    public override void _Ready()
    {
        var colliderShape = new CircleShape2D
        {
            Radius = _shipConfiguration.ColliderRadius
        };
        var collisionShape = new CollisionShape2D { Shape = colliderShape };
        _body.AddChild(collisionShape);

        var entityReference = new EntityReference { Entity = RootEntity };
        _body.AddChild(entityReference);

        var sprite = new Sprite2D
        {
            Texture = _shipConfiguration.Texture,
            Scale = _shipConfiguration.TextureScaling.AsUniformVector(),
            TextureFilter = CanvasItem.TextureFilterEnum.Nearest
        };
        _body.AddChild(sprite);

        AddChild(_body);
    }

    #region IShip

    public void Move(Direction movementDirection)
    {
        var speed = _shipConfiguration.Speed;
        _body.Velocity = (speed * movementDirection).VectorValue;
        _body.MoveAndSlide();
    }

    public void FireProjectile()
    {
        if (!_state.CanFire)
            return;

        _state.SetFireCooldown();

        var projectile = new Projectile
        {
            Source = RootEntity,
            Position = _body.GlobalPosition,
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
