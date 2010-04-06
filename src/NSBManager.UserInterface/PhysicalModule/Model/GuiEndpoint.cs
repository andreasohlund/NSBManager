using System.Windows;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public class GuiEndpoint : DependencyObject
    {
        //public string Id
        //{
        //    get
        //    {
        //        return (string)GetValue(EndpointIdProperty);
        //    }
        //    set
        //    {
        //        SetValue(EndpointIdProperty, value);
        //    }
        //}

        //public static readonly DependencyProperty EndpointIdProperty = DependencyProperty.Register("Id", typeof(string),
        //                                                                                     typeof(GuiEndpoint),
        //                                                                                     new UIPropertyMetadata(""));

        //public string Name
        //{
        //    get
        //    {
        //        return (string)GetValue(NameProperty);
        //    }
        //    set
        //    {
        //        SetValue(NameProperty, value);
        //    }
        //}

        //public static readonly DependencyProperty NameProperty = DependencyProperty.Register("Name", typeof(string),
        //                                                                                     typeof(GuiEndpoint),
        //                                                                                     new UIPropertyMetadata(""));

        public string Id { get; set; }
        private string name;
        public string Name
        {
            get { return ParseNameFromId(Id); }
            set { name = value; }
        }

        //Todo: Switch from strings
        public string Status { get; set; }
        public string HostType { get; set; }

        private string ParseNameFromId(string id)
        {
            var strings = id.Split('@');
            return strings[0];
        }
    }
}