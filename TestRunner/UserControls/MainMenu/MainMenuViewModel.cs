using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            newDialog.ShowDialog();
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
