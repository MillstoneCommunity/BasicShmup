using BasicShmup.Dynamics;
using Godot;

namespace BasicShmup.Entities.Projectiles;

public interface IProjectileConfiguration
{
    public Speed Speed { get; }

    public Texture2D Texture { get; }
}
