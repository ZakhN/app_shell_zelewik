using app_shell_zelewik.Models;
using app_shell_zelewik.Views;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace app_shell_zelewik.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        private Item _selectedItem;

        public ObservableCollection<Item> Items { get; }
        public Command LoadItemsCommand { get; }
        public Command AddItemCommand { get; }
        public Command<Item> ItemTapped { get; }
        public Command OpenSettingsCommand { get; set; }

        public INavigation Navigation { get; set; }

        private int itemsCount;
        public int ItemsCount { get { return itemsCount; } set { SetProperty(ref itemsCount, value); } }

        public ItemsViewModel()
        {
            Title = "Tasks";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            ItemTapped = new Command<Item>(OnItemSelected);

            AddItemCommand = new Command(OnAddItem);

            OpenSettingsCommand = new Command(OpenSettingsPage);
        }

        private void OpenSettingsPage()
        {
            Navigation.PushAsync(new SettingsPage());
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

            ItemsCount = Items.Count;

            return;
        }

        public void OnAppearing(bool load)
        {
            if (load)
            {
                Task task = Task.Run(() => ExecuteLoadItemsCommand());
                task.Wait();
            }
            IsBusy = true;
            SelectedItem = null;
        }

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        private void OnAddItem(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
            Navigation.PushAsync(new NewItemPage(this));
        }

        async void OnItemSelected(Item item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.Id}&CategoryName={item.Category}");
        }

        public int GetCurrentMaxId()
        {
            int maxId = 0;
            if (Items.Count > 0)
            {
                maxId = Items.Max(x => x.Id);
            }

            return maxId;
        }
    }
}