using app_shell_zelewik.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace app_shell_zelewik.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = new LoginViewModel();
        }
    }
}