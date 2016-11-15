using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Serialization;

namespace TestRunner.Commands
{
    public delegate void ProjectLoadedHandler(Project projectLoaded);

    public class OpenProjectCommand : ICommand
    {
        public static event ProjectLoadedHandler ProjectLoaded;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Reads the project from the inputted filepath and loads it into the ProjectManager.
        /// Calls the ProjectLoaded event.
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            string filePath = parameter as string;
            
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                Project loadedProject = (Project)serializer.Deserialize(reader);
                loadedProject.FilePath = filePath;

                ProjectLoaded?.Invoke(loadedProject);
            }
        }
    }
}
