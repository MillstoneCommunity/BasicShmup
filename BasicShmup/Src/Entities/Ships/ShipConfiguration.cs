using System.ComponentModel.DataAnnotations;
using BasicShmup.Dynamics;
using BasicShmup.ServiceProviders.Configurations;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Entities.Ships;

[GlobalClass]
public partial class ShipConfiguration : Resource, IConfiguration, IShipConfiguration
{
    [Export]
    [Required]
    public Texture2D Texture { get; set; } = ResourceLoader.Load<Texture2D>("res://Resources/ErrorTexture.png");

    [Export]
    public float TextureScaling { get; set; } = 1;

    [Export]
    private float _speed = 640;
    public Speed Speed => _speed;

    [Export]
    public float ColliderRadius { get; set; } = 64;

    [Export]
    private int _health = 3;
    public Health Health => _health;

    [Export]
    private int _damageOnCollision = 1;
    public Damage DamageOnCollision => _damageOnCollision;

    public void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IShipConfiguration>(this);
    }
}
