using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using app_shell_zelewik.Models;
using app_shell_zelewik.Views;
using app_shell_zelewik.ViewModels;

namespace app_shell_zelewik.ViewModels
{
    public class CategoriesViewModel : BaseViewModel
    {
        private Category selectedCategory;

        public ObservableCollection<Category> Categories { get; }
        public Command LoadCategoriesCommand { get; }
        public Command AddCategoryCommand { get; }
        public Command<Category> CategoryTapped { get; }

        public INavigation Navigation { get; set; }

        public CategoriesViewModel()
        {
            Title = "Categories";
            Categories = new ObservableCollection<Category>();
            LoadCategoriesCommand = new Command(async () => await ExecuteLoadCategoriesCommand());
            CategoryTapped = new Command<Category>(OnCategorySelected);
            AddCategoryCommand = new Command(OnAddCategory);
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;

            try
            {
                Categories.Clear();
                var categories = await DataStore.GetCategoriesAsync(true);
                foreach (var category in categories)
                {
                    Categories.Add(category);
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
        }

        public void OnAppearing()
        {
            IsBusy = true;
            selectedCategory = null;
        }

        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                SetProperty(ref selectedCategory, value);
                OnCategorySelected(value);
            }
        }

        private void OnAddCategory(object obj)
        {
            //await Shell.Current.GoToAsync(nameof(NewItemPage));
            Navigation.PushAsync(new NewCategoryPage(this));
        }

        async void OnCategorySelected(Category category)
        {
            if (category == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(CategoryDetailPage)}?{nameof(CategoryDetailViewModel.CategoryId)}={category.Id}");
        }

        public int GetCurrentMaxId()
        {
            int maxId = 0;
            if (Categories.Count > 0)
            {
                Debug.WriteLine("Categories count more than " + Categories.Max(x => x.Id));
                maxId = Categories.Max(x => x.Id);
            }
            Debug.WriteLine(Categories.Count);
            Debug.WriteLine($"CurrentMaxId {maxId}");

            return maxId;
        }
    }
}
