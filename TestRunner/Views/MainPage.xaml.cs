﻿using System;
using System.Diagnostics;
using System.IO;
using TestRunner.Models;
using TestRunner.TestRunnerService;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            TestRunnerServiceClient client = new TestRunnerServiceClient();

            int x = await client.StartTestingAsync("");
        }
    }
}
