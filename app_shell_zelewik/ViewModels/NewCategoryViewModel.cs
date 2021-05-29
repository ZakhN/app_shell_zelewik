using app_shell_zelewik.Models;
using System;
using Xamarin.Forms;

namespace app_shell_zelewik.ViewModels
{
    public class NewCategoryViewModel : BaseViewModel
    {
        private int id;
        private string title;
        private string description;
        private int successRate;
        public Command SaveCommand { get; }
        public Command CancelCommand { get; }
        public int CurrentMaxId { get; set; }
        public NewCategoryViewModel()
        {
            SaveCommand = new Command(OnSave, ValidateSave);
            CancelCommand = new Command(OnCancel);
            this.PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }

        private bool ValidateSave()
        {
            return !String.IsNullOrEmpty(title)
            && !String.IsNullOrEmpty(description);
        }

        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
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

        private async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void OnSave()
        {
            Category newCategory = new Category()
            {
                Id = CurrentMaxId + 1,
                Title = Title,
                Description = Description,
                SuccessRate = SuccessRate
            };
            await DataStore.AddCategoryAsync(newCategory);

            await Shell.Current.GoToAsync("..");
        }
    }
}
