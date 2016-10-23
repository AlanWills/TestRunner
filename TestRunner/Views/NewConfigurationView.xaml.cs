﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TestRunner.TestRunnerService;
using TestRunnerLibrary;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewConfigurationView : Page
    {
        #region Properties and Fields

        private NewConfigurationViewModel NewConfig { get; set; }

        #endregion

        public NewConfigurationView()
        {
            NewConfig = new NewConfigurationViewModel();
            NewConfig.PropertyChanged += NewConfig_PropertyChanged;
            DataContext = NewConfig;
            InitializeComponent();
        }

        private async void NewConfig_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "FullPathToDll" ||
                e.PropertyName == "OutputFileFullPath" ||
                e.PropertyName == "ErrorFileFullPath")
            {
                StartButton.IsEnabled = await Task.Run(() =>
                {
                    return NewConfig.IsConfigurationValid;
                });
            }
        }

        private void CreateTestRunProcess(object sender, RoutedEventArgs e)
        {
            NewConfig.CreateTestRunProcess();
        }
    }
}
