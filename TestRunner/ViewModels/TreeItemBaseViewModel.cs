namespace TestRunner.ViewModels
{
    public abstract class TreeItemBaseViewModel
    {
        #region Properties and Fields

        public bool IsExpanded { get; set; }

        #endregion

        public abstract void RefreshOnProjectChanged(ProjectChangedEventArgs args);
    }
}
