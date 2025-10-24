using BasicShmup.Dynamics;
using BasicShmup.Entities.Battle;
using BasicShmup.Entities.Projectiles;
using BasicShmup.Events;
using Godot;

namespace BasicShmup.Entities.Ships;

public partial class Ship : Node, IShip
{
    private readonly IEventSender _eventSender = EventBroker.Instance;

    private readonly CircleShape2D _colliderShape = new()
    {
        Radius = 75
    };

    private readonly CharacterBody2D _body = new()
    {
        MotionMode = CharacterBody2D.MotionModeEnum.Floating
    };

    private readonly Sprite2D _sprite = new()
    {
        Texture = ResourceLoader.Load<Texture2D>("res://Resources/ErrorTexture.png"),
        TextureFilter = CanvasItem.TextureFilterEnum.Nearest
    };

    public required IEntity RootEntity { get; init; }

    public Speed Speed { get; set; } = 750;

    public float Radius
    {
        get => _colliderShape.Radius;
        set => _colliderShape.Radius = value;
    }

    public Texture2D Texture
    {
        get => _sprite.Texture;
        set => _sprite.Texture = value;
    }

    public Vector2 Position
    {
        get => _body.Position;
        set => _body.Position = value;
    }

    public override void _EnterTree()
    {
        var collisionShape = new CollisionShape2D { Shape = _colliderShape };
        _body.AddChild(collisionShape);

        var entityReference = new EntityReference { Entity = RootEntity };
        _body.AddChild(entityReference);

        _body.AddChild(_sprite);

        AddChild(_body);
    }

    #region IShip

    public void Move(Direction movementDirection)
    {
        _body.Velocity = (Speed * movementDirection).VectorValue;
        _body.MoveAndSlide();
    }

    public void FireProjectile()
    {
        var projectile = new Projectile
        {
            Source = RootEntity,
            Position = _body.GlobalPosition
        };

        _eventSender.Send(new SpawnBattleNodeEvent(projectile));
    }

    #endregion
}
