namespace BasicShmup.Events;

public interface IEventHandler<in TEvent>
{
    void Handle(TEvent @event);
}
