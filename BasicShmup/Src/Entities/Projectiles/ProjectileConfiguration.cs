using System.ComponentModel.DataAnnotations;
using BasicShmup.Dynamics;
using BasicShmup.Entities.Ships;
using BasicShmup.ServiceProviders.Configurations;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Entities.Projectiles;

[GlobalClass]
public partial class ProjectileConfiguration : Resource, IConfiguration, IProjectileConfiguration
{
    [Export]
    [Required]
    public Texture2D Texture { get; set; } = ResourceLoader.Load<Texture2D>("res://Resources/ErrorTexture.png");

    [Export]
    public float TextureScaling { get; set; } = 1;

    [Export]
    public float ColliderRadius { get; set; } = 64;

    [Export]
    private float _speed = 1280;
    public Speed Speed => _speed;

    [Export]
    private int _damage = 1;

    public Damage Damage => _damage;

    public void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProjectileConfiguration>(this);
    }
}
