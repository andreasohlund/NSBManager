using System;
using System.Windows;

namespace NSBManager.UserInterface
{
    public class ViewModelLoader
    {
        //public static readonly DependencyProperty FactoryTypeProperty =
        //    DependencyProperty.RegisterAttached("FactoryType", typeof (Type), typeof (ViewModelLoader),
        //                                        new FrameworkPropertyMetadata((Type) null),
        //                                        new PropertyChangedCallback(OnFactoryTypeChanged));


        public static readonly DependencyProperty FactoryTypeProperty = DependencyProperty.RegisterAttached("FactoryType", typeof(Type), typeof(ViewModelLoader),
                                                                                new FrameworkPropertyMetadata((Type)null,
                                                                                new PropertyChangedCallback(OnFactoryTypeChanged)));

        private static void OnFactoryTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var test = e.NewValue;
        }

        public static Type GetFactoryType(DependencyObject d)
        {
            return (Type)d.GetValue(FactoryTypeProperty);
        }

        public static void SetFactoryType(DependencyObject d, Type value)
        {
            d.SetValue(FactoryTypeProperty, value);
        }

    }
}