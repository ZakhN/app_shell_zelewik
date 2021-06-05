using app_shell_zelewik.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : ContentPage
    {
        CategoriesViewModel categoriesViewModel;
        ItemsViewModel itemsViewModel;

        public AboutPage()
        {
            InitializeComponent();
            categoriesViewModel = new CategoriesViewModel();
            itemsViewModel = new ItemsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            itemsViewModel.OnAppearing(true);
            categoriesViewModel.OnAppearing(true);

            ItemsNumber.Text = itemsViewModel.ItemsCount.ToString();
            CategoriesNumber.Text = categoriesViewModel.CategoriesCount.ToString();
        }
    }
}