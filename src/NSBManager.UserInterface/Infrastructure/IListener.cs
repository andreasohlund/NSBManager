namespace NSBManager.UserInterface.Infrastructure
{
    public interface IListener<T>
    {
        void Handle(T message);
    }
}