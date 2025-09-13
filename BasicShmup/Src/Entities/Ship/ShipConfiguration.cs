using System.ComponentModel.DataAnnotations;
using BasicShmup.addons.ResourcePreview;
using BasicShmup.Configurations;
using BasicShmup.Domain.Dynamics;
using BasicShmup.Domain.Entities.Ship;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace BasicShmup.Entities.Ship;

[Tool]
[GlobalClass]
public partial class ShipConfiguration :
    ConfigurationResource,
    IResourcePreview,
    IShipViewFactory,
    IShipConfiguration
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

    public Node GetPreviewNode()
    {
        var preview = Create(NullShip.Instance);

        // disable process, to not have the ShipController fail to get input
        preview.ProcessMode = Node.ProcessModeEnum.Disabled;

        return preview;
    }

    public ShipView Create(IShip ship)
    {
        return new ShipView
        {
            Ship = ship,
            Texture = _texture
        };
    }

    public override void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IShipViewFactory>(this);
        serviceCollection.AddSingleton<IShipConfiguration>(this);
    }
}
