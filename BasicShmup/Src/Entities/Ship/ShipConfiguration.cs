using System.ComponentModel.DataAnnotations;
using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Entities.Ship;
using BasicShmup.ServiceProviders.Configurations;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Entities.Ship;

[Tool]
[GlobalClass]
public partial class ShipConfiguration :
    Resource,
    IShipViewFactory,
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

    // todo remove
    public ShipView Create(IShip ship)
    {
        return new ShipView
        {
            Ship = ship,
            Texture = _texture
        };
    }

    // todo remove
    public void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IShipViewFactory>(this);
        serviceCollection.AddSingleton<IShipConfiguration>(this);
    }
}
