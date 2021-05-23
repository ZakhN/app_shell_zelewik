using app_shell_zelewik.Models;
using app_shell_zelewik.ViewModels;
using System;
using Xamarin.Forms;

namespace app_shell_zelewik.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        ItemsViewModel itemsViewModel;

        public NewItemPage(ItemsViewModel viewModel)
        {
            InitializeComponent();
            itemsViewModel = viewModel;
            int CurrentMaxId = itemsViewModel.GetCurrentMaxId();
            BindingContext = new NewItemViewModel() { CurrentMaxId = CurrentMaxId };
        }

        private void DatePickerDate_DateSelected(object sender, DateChangedEventArgs e)
        {

        }

        void PickerCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
        }
    }
}