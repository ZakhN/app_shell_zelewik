using app_shell_zelewik.Models;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace app_shell_zelewik.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        private int id;
        private string text;
        private string description;
        private string date;
        private int importance;
        private string category;
        public int CurrentMaxId { get; set; }
        public ObservableCollection<Category> Categories { get; }
        public Command LoadCategoriesCommand { get; }
        public Category SelectedDepartment { get; set; }

        public NewItemViewModel()
        {
            Categories = new ObservableCollection<Category>();
            ExecuteLoadCategoriesCommand();
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged +=
                (_, __) => SaveCommand.ChangeCanExecute();
        }

        async Task ExecuteLoadCategoriesCommand()
        {
            IsBusy = true;
            Debug.WriteLine("ExecuteLoadCategoriesCommand");
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

        private bool ValidateSave()
        {
            return !String.IsNullOrWhiteSpace(text)
                && !String.IsNullOrWhiteSpace(description)
                && !String.IsNullOrWhiteSpace(date);
        }

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        public string Text
        {
            get => text;
            set => SetProperty(ref text, value);
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
            set => SetProperty(ref category, value);
        }

        public Command SaveCommand { get; }
        public Command CancelCommand { get; }

        private async void OnCancel()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Item newItem = new Item()
            {
                Id = CurrentMaxId + 1,
                Text = Text,
                Description = Description,
                Date = Date,
                Importance = Importance,
                Category = SelectedDepartment.Title
            };

            await DataStore.AddItemAsync(newItem);

            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}
