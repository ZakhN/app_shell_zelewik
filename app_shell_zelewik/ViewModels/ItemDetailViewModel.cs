using app_shell_zelewik.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace app_shell_zelewik.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [QueryProperty(nameof(CategoryName), nameof(CategoryName))]
    public class ItemDetailViewModel : BaseViewModel
    {
        private int itemId;
        private string text;
        private string description;
        private string date;
        private int importance;
        private string category;
        private Item item;
        public int Id { get; set; }
        private string categoryName { get; set; }
        private Category selectedDepartment { get; set; }

        public ObservableCollection<Category> Categories { get; }

        public Command DeleteCommand { get; set; }
        public Command SaveCommand { get; set; }

        public ItemDetailViewModel()
        {

            DeleteCommand = new Command(async () => await Delete());
            SaveCommand = new Command(async () => await Save());
            Categories = new ObservableCollection<Category>();
            ExecuteLoadCategoriesCommand();
        }


        async Task ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;
            try
            {
                Categories.Clear();
                var categories = await DataStore.GetCategoriesAsync();
                foreach (var category in categories)
                {
                    Categories.Add(category);
                }
                var categorySelected = await DataStore.GetCategoryByTitleAsync(CategoryName);
                SelectedDepartment = categorySelected;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task Delete()
        {
            await DataStore.DeleteItemAsync(item.Id);
        }

        async Task Save()
        {
            Debug.WriteLine("Item for update 1 ");
            Debug.Write(item);
            Item newItem = new Item()
            {
                Id = Id,
                Text = Text,
                Description = Description,
                Date = Date,
                Importance = Importance,
                Category = SelectedDepartment.Title
            };
            await DataStore.UpdateItemAsync(newItem);
        }

        public Category SelectedDepartment
        {
            get
            {
                return selectedDepartment;
            }
            set
            {
                selectedDepartment = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
        }

        public string CategoryName
        {
            get
            {
                return categoryName;
            }
            set
            {
                categoryName = value;
            }
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public int Importance
        {
            get => importance;
            set => SetProperty(ref importance, value);
        }

        public string Category
        {
            get => category;
            set => category = value;
        }

        public int ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value);
            }
        }

        public async void LoadItemId(int itemId)
        {
            try
            {
                item = await DataStore.GetItemAsync(itemId);
                Id = item.Id;
                Text = item.Text;
                Description = item.Description;
                Date = item.Date;
                Importance = item.Importance;
                Category = item.Category;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Load Item: " + ex);
            }
        }
    }
}
