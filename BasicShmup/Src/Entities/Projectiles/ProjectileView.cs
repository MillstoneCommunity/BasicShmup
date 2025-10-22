using Godot;

namespace BasicShmup.Entities.Projectiles;

[GlobalClass]
public partial class ProjectileView : Node2D
{
    public ProjectileView()
    {
        AddChild(CreateSprite());
        AddChild(CreateCollider());
    }

    private static Node CreateSprite()
    {
        return new Sprite2D
        {
            Texture = ResourceLoader.Load<Texture2D>("res://icon.svg")
        };
    }

    private static Node CreateCollider()
    {
        var collisionShape = new CollisionShape2D()
        {
            Shape = new CircleShape2D
            {
                Radius = 75
            }
        };
        var staticBody = new CharacterBody2D();
        staticBody.AddChild(collisionShape);

        return staticBody;
    }
}
