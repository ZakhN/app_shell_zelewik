using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using app_shell_zelewik.ViewModels;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryDetailPage : ContentPage
    {
        public CategoryDetailPage()
        {

            InitializeComponent();
            BindingContext = new CategoryDetailViewModel();
        }
        async void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}