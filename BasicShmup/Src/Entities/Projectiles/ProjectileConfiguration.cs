using System.ComponentModel.DataAnnotations;
using BasicShmup.Dynamics;
using BasicShmup.ServiceProviders.Configurations;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Entities.Projectiles;

[GlobalClass]
public partial class ProjectileConfiguration : Resource, IConfiguration, IProjectileConfiguration
{
    [Export]
    private float _speed = 1400;

    public Speed Speed => _speed;

    [Export]
    [Required]
    public Texture2D Texture { get; set; } = ResourceLoader.Load<Texture2D>("res://Resources/ErrorTexture.png");

    public void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IProjectileConfiguration>(this);
    }
}
