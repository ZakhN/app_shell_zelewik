using app_shell_zelewik.ViewModels;
using Xamarin.Forms;

namespace app_shell_zelewik.Views
{
    public partial class AboutPage : ContentPage
    {
        CategoriesViewModel viewModel;
     
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CategoriesViewModel();
        }
    }
}