using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalsPage : ContentPage
    {
        public GoalsPage()
        {
            InitializeComponent();
        }

        private async void GoToGoal(object sender, EventArgs e)
        {
            GoalDetailPage page = new GoalDetailPage();
            await Navigation.PushAsync(page);
            page.DisplayGoalPageStack();
        }
    }
}