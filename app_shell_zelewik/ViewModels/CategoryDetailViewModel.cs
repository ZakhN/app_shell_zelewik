using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using app_shell_zelewik.Models;

namespace app_shell_zelewik.ViewModels
{
    [QueryProperty(nameof(CategoryId), nameof(CategoryId))]
    public class CategoryDetailViewModel : BaseViewModel
    {
        private int categoryId;
        private string title;
        private string description;
        private int successRate;
        private Category category;
        public int Id { get; set; }

        public Command DeleteCommand { get; set; }
        public Command SaveCommand { get; set; }

        public CategoryDetailViewModel()
        {
            DeleteCommand = new Command(async () => await Delete());
            SaveCommand = new Command(async () => await Save());
            Debug.WriteLine("CategoryDetailViewvModel Constructor");
        }

        async Task Delete()
        {
            await DataStore.DeleteCategoryAsync(category.Id);
        }

        async Task Save()
        {
            Category newCategory = new Category()
            {
                Id = Id,
                Title = Title,
                Description = Description,
                SuccessRate = SuccessRate
            };
            await DataStore.UpdateCategoryAsync(newCategory);
        }

        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }
        public int SuccessRate
        {
            get => successRate;
            set => SetProperty(ref successRate, value);
        }

        public int CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                Debug.WriteLine("Load category details with id " + value);
                LoadCategoryId(value);
            }
        }

        public async void LoadCategoryId(int categoryId)
        {
            try
            {
                category = await DataStore.GetCategoryAsync(categoryId);
                Debug.WriteLine("Category response: " + category.Title);
                Id = category.Id;
                Title = category.Title;
                Description = category.Description;
                SuccessRate = category.SuccessRate;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to load Category");
            }
        }
    }
}
