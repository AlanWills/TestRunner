﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TestRunner.Views;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace TestRunner.UserControls
{
    public sealed partial class NavBar : UserControl
    {
        public NavBar()
        {
            this.InitializeComponent();
        }

        private void NavigateToMainPage(object sender, RoutedEventArgs args)
        {
            bool result = (Window.Current.Content as Frame).Navigate(typeof(MainPage));
        }

        private void NavigateToAppSettingsView(object sender, RoutedEventArgs args)
        {
            bool result = (Window.Current.Content as Frame).Navigate(typeof(AppSettingsView));
        }
    }
}