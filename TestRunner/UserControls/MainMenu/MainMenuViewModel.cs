using Microsoft.Win32;
using System.IO;
using System.Windows;
using TestRunner.Views;

namespace TestRunner.UserControls
{
    public class MainMenuViewModel
    {
        #region File Menu Item Callbacks

        public void New()
        {
            ProjectManager.NewProjectCommand.Execute(null);
        }

        public void Open()
        {
            OpenFileDialog filePicker = new OpenFileDialog();
            filePicker.DefaultExt = Project.FileExtension;
            filePicker.InitialDirectory = Directory.GetCurrentDirectory();

            bool? result = filePicker.ShowDialog();
            if (result.HasValue && result.Value)
            {
                ProjectManager.OpenProjectCommand.Execute(filePicker.FileName);
            }
        }

        public void Save()
        {

        }

        public void Minimize()
        {
            Application.Current.MainWindow.Hide();
        }

        public void Exit()
        {
            Application.Current.MainWindow.Close();
        }

        #endregion
    }
}