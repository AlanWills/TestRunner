using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestRunner.UserControls
{
    /// <summary>
    /// Interaction logic for CustomTabItem.xaml
    /// </summary>
    public partial class CustomTabItem : TabItem
    {
        public CustomTabItem()
        {
            InitializeComponent();
            MinWidth = 100;
            MaxWidth = 100;
        }

        private void CloseTab(object sender, MouseButtonEventArgs e)
        {
            (Parent as TabControl).Items.Remove(this);
        }
    }
}
