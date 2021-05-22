using app_shell_zelewik.ViewModels;
using System;
using Xamarin.Forms;

namespace app_shell_zelewik.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailViewModel viewModel;

        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ItemDetailViewModel();
            //viewModel.SelectedCategory();
        }


        private void DatePickerDate_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        async void GoBack(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}