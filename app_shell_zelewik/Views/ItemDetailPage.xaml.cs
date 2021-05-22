using app_shell_zelewik.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace app_shell_zelewik.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}