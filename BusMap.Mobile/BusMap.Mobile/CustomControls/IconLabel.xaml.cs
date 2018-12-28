using Plugin.Iconize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusMap.Mobile.CustomControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconWithLabel : StackLayout
    {
        public static readonly BindableProperty TextProperty
            = BindableProperty.Create(nameof(Text), typeof(string), typeof(IconWithLabel));

        public static readonly BindableProperty IconProperty
            = BindableProperty.Create(nameof(Icon), typeof(string), typeof(IconWithLabel));

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }




        public IconWithLabel()
        {
            InitializeComponent();
        }
    }
}