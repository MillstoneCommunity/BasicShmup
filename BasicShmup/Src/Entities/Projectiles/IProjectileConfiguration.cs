using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships.States;
using Godot;

namespace BasicShmup.Entities.Projectiles;

public interface IProjectileConfiguration
{
    Texture2D Texture { get; }
    float TextureScaling { get; }
    float ColliderRadius { get; }
    Speed Speed { get; }
    Damage Damage { get; }
}
