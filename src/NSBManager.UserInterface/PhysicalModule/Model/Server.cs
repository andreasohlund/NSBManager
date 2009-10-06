using System.Windows;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public class Server : DependencyObject
    {
        public string Name
        {
            get
            {
                return (string) GetValue(NameProperty);
            }
            set
            {
                SetValue(NameProperty, value);
            }
        }

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof (string),
                                                                                             typeof (Server),
                                                                                             new UIPropertyMetadata(""));

        
    }
}