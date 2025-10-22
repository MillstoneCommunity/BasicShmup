using System.ComponentModel.DataAnnotations;
using BasicShmup.Domain.Dynamics;
using BasicShmup.ServiceProviders.Configurations;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Entities.Ships;

[Tool]
[GlobalClass]
public partial class ShipConfiguration :
    Resource,
    IShipConfiguration,
    IConfiguration
{
    [Export]
    private float _speed = 750;

    [Export]
    private float _radius = 75;

    [Export]
    [Required]
    private Texture2D _texture = null!;

    public Speed Speed => _speed;
    public float Radius => _radius;
    public Texture2D Texture => _texture;

    public void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IShipConfiguration>(this);
    }
}
