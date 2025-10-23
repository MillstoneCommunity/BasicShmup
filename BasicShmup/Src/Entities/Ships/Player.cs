using BasicShmup.Entities.Projectiles;
using BasicShmup.Events;
using BasicShmup.Input.Controllers;
using Godot;

namespace BasicShmup.Entities.Ships;

[GlobalClass]
public partial class Player : Node2D, IEntity, IEventHandler<ProjectileHitEvent>
{
    private readonly ICharacterBodyControllerStrategy _controllerStrategy = new CharacterBodyControllerStrategy();
    private readonly CharacterBody2D _body;

    [Export]
    private float _speed = 750;

    [Export]
    private float _radius = 75;

    [Export]
    private Texture2D _texture = ResourceLoader.Load<Texture2D>("res://Resources/ErrorTexture.png");

    public Player()
    {
        _body = CreateBody();

        var sprite = new Sprite2D
        {
            Texture = _texture,
            TextureFilter = TextureFilterEnum.Nearest,
            Scale = new Vector2(4, 4)
        };
        _body.AddChild(sprite);

        AddChild(_body);
    }

    private CharacterBody2D CreateBody()
    {
        var body = new CharacterBody2D
        {
            MotionMode = CharacterBody2D.MotionModeEnum.Floating
        };

        var collisionShape = new CollisionShape2D
        {
            Shape = new CircleShape2D
            {
                Radius = _radius
            }
        };
        body.AddChild(collisionShape);

        var entityReference = new EntityReference
        {
            Entity = this
        };
        body.AddChild(entityReference);

        return body;
    }

    public override void _PhysicsProcess(double delta)
    {
        _controllerStrategy.Move(_body, _speed);
    }

    public void Handle(ProjectileHitEvent projectileHitEvent)
    {
        GD.Print("Player was hit");
    }
}
