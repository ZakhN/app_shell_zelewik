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
    public class CategoriesViewModel : BaseViewModel
    {
        private Category selectedCategory;

        private ObservableCollection<Category> сategories;

        public ObservableCollection<Category> Categories { get { return сategories; } set { SetProperty(ref сategories, value); } }
        public Command LoadCategoriesCommand { get; }
        public Command AddCategoryCommand { get; }
        public Command<Category> CategoryTapped { get; }
        private int categoryCount;
        public int CategoriesCount { get { return categoryCount; } set { SetProperty(ref categoryCount, value); } }
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
  
            CategoriesCount = Categories.Count;

            return;
        }


        public void OnAppearing()
        {
            IsBusy = true;
            selectedCategory = null;
        }

        public void OnAppearing(bool load)
        {
            //Task task = Task.Run(() => ExecuteLoadCategoriesCommand());
            //task.Wait();
            ExecuteLoadCategoriesCommand();
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
                maxId = Categories.Max(x => x.Id);
            }

            return maxId;
        }
    }
}
