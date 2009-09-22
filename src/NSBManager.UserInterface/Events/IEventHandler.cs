namespace NSBManager.UserInterface.Events
{
    public interface IEventHandler<T>
    {
        void Handle(T message);
    }
}