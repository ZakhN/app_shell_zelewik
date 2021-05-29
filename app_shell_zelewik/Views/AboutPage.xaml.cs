using app_shell_zelewik.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        CategoriesViewModel viewModel;
     
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new CategoriesViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing(true);
        }
    }
}