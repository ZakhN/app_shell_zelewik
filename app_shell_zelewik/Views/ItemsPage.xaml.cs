using app_shell_zelewik.ViewModels;
using Xamarin.Forms;

namespace app_shell_zelewik.Views
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel _viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ItemsViewModel() { Navigation = this.Navigation };

            var settingViewModel = new SettingsViewModel();
            settingViewModel.LoadSettings();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing(false);
        }
    }
}