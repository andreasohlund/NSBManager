namespace NSBManager.Infrastructure.EventAggregator
{
    public interface IListener<T>
    {
        void Handle(T message);
    }
}