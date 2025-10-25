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
    private float _speed = 750;

    public Speed Speed => _speed;

    [Export]
    public float ColliderRadius { get; set; } = 75;

    [Export]
    [Required]
    public Texture2D Texture { get; set; } = ResourceLoader.Load<Texture2D>("res://Resources/ErrorTexture.png");

    public void Register(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IShipConfiguration>(this);
    }
}
