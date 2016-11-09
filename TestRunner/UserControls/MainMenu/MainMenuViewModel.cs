using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestRunner.UserControls
{
    public class MainMenuViewModel
    {
        #region File Menu Item Callbacks

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
