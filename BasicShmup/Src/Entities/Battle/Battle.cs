using BasicShmup.Events;
using BasicShmup.ServiceProviders;
using Godot;

namespace BasicShmup.Entities.Battle;

[GlobalClass]
public partial class Battle : Node, IEventHandler<SpawnBattleNodeEvent>
{
    [Inject]
    private readonly IEventReceiver _eventReceiver = null!;

    public override void _Ready()
    {
        _eventReceiver.RegisterEventHandler(this);
    }

    public override void _ExitTree()
    {
        _eventReceiver.TryRemoveEventHandler(this);
    }

    public void Handle(SpawnBattleNodeEvent @event) => AddChild(@event.Node);
}
