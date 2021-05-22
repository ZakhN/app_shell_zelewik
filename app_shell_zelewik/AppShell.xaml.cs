﻿using app_shell_zelewik.ViewModels;
using app_shell_zelewik.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace app_shell_zelewik
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//Main/Tasks/ItemsPage");
        }
    }
}