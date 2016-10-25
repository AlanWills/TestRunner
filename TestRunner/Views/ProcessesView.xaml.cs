using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TestRunner.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProcessesView : Page
    {
        #region Properties and Fields

        private ProcessesViewModel ProcessesViewModel { get; set; }

        #endregion

        public ProcessesView()
        {
            ProcessesViewModel = new ProcessesViewModel();
            DataContext = ProcessesViewModel;
            InitializeComponent();
            ProcessesViewModel.PropertyChanged += UpdateBuildResults;
        }
        
        private void UpdateBuildResults(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == SelectedProcessOutput.Name)
            {
                SelectedProcessOutput.Text = ProcessesViewModel.SelectedProcessOutput;
            }
            else if (e.PropertyName == Processes.Name)
            {
                foreach (string item in ProcessesViewModel.Processes)
                {
                    Processes.Items.Add(item);
                }
            }
        }

        private void Processes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string clickedItem = e.Source as string;
            string[] splitStr = clickedItem.Split(' ');
            string id = splitStr.Last();

            ProcessesViewModel.GetProcessOutput(ulong.Parse(id.Substring(1, id.Length - 2)));
        }
    }
}