using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TestRunner.Models;
using TestRunner.ViewModels;

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

        public void UpdateUIWithTestResult(TreeItemTestResultViewModel testResult)
        {
            foreach (UnitTestResult unitTestResult in testResult.UnitTests)
            {
                TextBlock unitTestName = new TextBlock();
                unitTestName.Text = unitTestResult.Name;
                unitTestName.Foreground = unitTestResult.Passed ? Brushes.LawnGreen : Brushes.Red;

                TestResults.Items.Add(unitTestName);
            }
        }
    }
}
