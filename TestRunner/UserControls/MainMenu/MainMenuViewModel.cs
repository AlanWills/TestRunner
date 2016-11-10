using System.Windows;
using TestRunner.Views;

namespace TestRunner.UserControls
{
    public class MainMenuViewModel
    {
        #region File Menu Item Callbacks

        public void New()
        {
            NewProjectDialogView newDialog = new NewProjectDialogView();
            bool? result = newDialog.ShowDialog();
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
