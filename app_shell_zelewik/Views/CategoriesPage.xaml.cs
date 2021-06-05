
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using app_shell_zelewik.ViewModels;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesPage : ContentPage
    {
        CategoriesViewModel viewModel;

        public CategoriesPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CategoriesViewModel() { Navigation = this.Navigation };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing(false);
        }
    }
}