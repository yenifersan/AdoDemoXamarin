using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Ejercicio10
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdoDemo : ContentPage
    {
        public AdoDemo()
        {
            InitializeComponent();
            string output = string.Empty;
            output = AdoExample.DoSomeDataAccess();
            var layout = new StackLayout { Padding = new Thickness(5, 10) };
            var label = new Label { Text = output, TextColor = Color.FromHex("#77d065"), FontSize = 20 };
            layout.Children.Add(label);
            this.Content = layout;
        }
    }
}

