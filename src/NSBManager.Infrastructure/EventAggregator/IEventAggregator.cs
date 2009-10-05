namespace NSBManager.Infrastructure.EventAggregator
{
    public interface IEventAggregator
    {
        void Publish<T>(T message);
        void Publish<T>() where T : new();
        void RegisterListener<T>(IListener<T> listener);
    }
}