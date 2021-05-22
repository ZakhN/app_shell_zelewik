using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailPage : ContentPage
    {
        public CategoryDetailPage()
        {
            InitializeComponent();
        }
        async void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}