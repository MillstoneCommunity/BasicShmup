namespace BasicShmup.Events;

public interface IEventReceiver
{
    public void RegisterEventHandler<TEvent>(IEventHandler<TEvent> eventHandler);
    public void TryRemoveEventHandler<TEvent>(IEventHandler<TEvent> eventHandler);
}