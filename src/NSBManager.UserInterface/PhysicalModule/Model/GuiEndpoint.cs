using System.Windows;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public class GuiEndpoint : DependencyObject
    {
        public string Id
        {
            get
            {
                return (string)GetValue(EndpointIdProperty);
            }
            set
            {
                SetValue(EndpointIdProperty, value);
            }
        }

        public static readonly DependencyProperty EndpointIdProperty = DependencyProperty.Register("Id", typeof(string),
                                                                                             typeof(GuiEndpoint),
                                                                                             new UIPropertyMetadata(""));

        public string Name
        {
            get
            {
                return (string)GetValue(NameProperty);
            }
            set
            {
                SetValue(NameProperty, value);
            }
        }

        public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string),
                                                                                             typeof(GuiEndpoint),
                                                                                             new UIPropertyMetadata(""));
    }
}