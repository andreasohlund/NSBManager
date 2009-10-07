using System;
using System.Windows;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class Endpoint : DependencyObject
    {
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
                                                                                             typeof(Endpoint),
                                                                                             new UIPropertyMetadata("EndpointName"));
        public string ServerName
        {
            get
            {
                return (string)GetValue(ServerNameProperty);
            }
            set
            {
                SetValue(ServerNameProperty, value);
            }
        }

        public static readonly DependencyProperty ServerNameProperty = DependencyProperty.Register("ServerName", typeof(string),
                                                                                                   typeof(Endpoint),
                                                                                                   new UIPropertyMetadata("ServerName"));

        //public string ServerName { get; set; }
    }
}