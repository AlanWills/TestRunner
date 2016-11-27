using System.Windows.Controls;
using System.Windows.Media;
using TestRunner.Models;
using TestRunner.ViewModels;

namespace TestRunner.UserControls
{
    /// <summary>
    /// Interaction logic for TestResultTabView.xaml
    /// </summary>
    public partial class TestResultTabView : TabItem
    {
        #region Properties and Fields

        private TestResultTabViewModel TestResultTab { get; set; }

        #endregion

        public TestResultTabView(TestResult testResult)
        {
            TestResultTab = new TestResultTabViewModel(testResult);
            DataContext = TestResultTab;

            InitializeComponent();
            MinWidth = 100;
            MaxWidth = 100;
            Name = testResult.Name.Replace(" ", "") + "Tab";
        }
    }
}
