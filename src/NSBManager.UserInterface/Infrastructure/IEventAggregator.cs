namespace NSBManager.UserInterface.Infrastructure
{
    public interface IEventAggregator
    {
        void Publish<T>(T message);
        void RegisterListener<T>(IListener<T> listener);
    }
}