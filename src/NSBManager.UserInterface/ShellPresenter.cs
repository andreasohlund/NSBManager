namespace NSBManager.UserInterface
{
    public class ShellPresenter
    {
        public IShellView View;

        public ShellPresenter(IShellView view)
        {
            View = view;
        }
    }
}