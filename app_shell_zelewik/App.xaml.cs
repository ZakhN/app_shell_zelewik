using app_shell_zelewik.Services;
using app_shell_zelewik.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app_shell_zelewik
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
