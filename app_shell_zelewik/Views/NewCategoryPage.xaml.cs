
using app_shell_zelewik.ViewModels;
using Xamarin.Forms;

namespace app_shell_zelewik.Views
{
    public partial class NewCategoryPage : ContentPage
    {

        CategoriesViewModel categoriesViewModel;

        public NewCategoryPage(CategoriesViewModel viewModel)
        {
            InitializeComponent();
            categoriesViewModel = viewModel;
            BindingContext = new NewCategoryViewModel() { CurrentMaxId = categoriesViewModel.GetCurrentMaxId() };
        }
    }
}