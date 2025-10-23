namespace BasicShmup.Events;

internal interface IEventHandler<in TEvent>
{
    void Handle(TEvent @event);
}