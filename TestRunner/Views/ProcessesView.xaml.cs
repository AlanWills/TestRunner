﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class ProcessesView : Page
    {
        #region Properties and Fields

        private ProcessesViewModel Processes { get; set; }

        #endregion

        public ProcessesView()
        {
            Processes = new ProcessesViewModel();
            DataContext = Processes;
            InitializeComponent();
            Processes.PropertyChanged += UpdateBuildResults;
        }

        private void UpdateBuildResults(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SelectedProcessOutput.Name)
            {
                SelectedProcessOutput.Text = Processes.SelectedProcessOutput;
            }
        }
    }
}
