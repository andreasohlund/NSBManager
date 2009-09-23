namespace NSBManager.UserInterface.Events
{
    public interface IListener<T>
    {
        void Handle(T message);
    }
}