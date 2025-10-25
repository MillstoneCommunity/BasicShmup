namespace BasicShmup.Events;

public interface IEventSender
{
    public void Send<TEvent>(TEvent @event);
}