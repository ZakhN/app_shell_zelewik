using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using app_shell_zelewik.ViewModels;
using app_shell_zelewik.Models;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        private SettingsViewModel settingsViewModel;
        public SettingsPage()
        {
            InitializeComponent();
            BindingContext = settingsViewModel = new SettingsViewModel();
        }

        private void OnPickerSelectionChanged(object sender, EventArgs e)
        {
            Picker picker = sender as Picker;
            Settings.Themes theme = (Settings.Themes)picker.SelectedItem;

            settingsViewModel.SelectTheme(theme);
        }
    }
}