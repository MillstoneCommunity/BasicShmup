using BasicShmup.Events;
using Godot;

namespace BasicShmup.Entities.Battle;

[GlobalClass]
public partial class Battle : Node, IEventHandler<SpawnBattleNodeEvent>
{
    private readonly IEventReceiver _eventReceiver = EventBroker.Instance;

    public override void _EnterTree()
    {
        _eventReceiver.RegisterEventHandler(this);
    }

    public override void _ExitTree()
    {
        _eventReceiver.TryRemoveEventHandler(this);
    }

    public void Handle(SpawnBattleNodeEvent @event) => AddChild(@event.Node);
}
