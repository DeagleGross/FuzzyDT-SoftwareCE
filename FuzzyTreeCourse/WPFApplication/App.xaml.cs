using FuzzyTreeLib.Models.Counters;
using System.Windows;
using FuzzyTreeLib.Models;
using FuzzyTreeLib.Models.Tree;

namespace FuzzyTreeWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainController mainController;

        /// <summary>
        /// Accessor to mainController
        /// </summary>
        public MainController MainController
        {
            get
            {
                if (mainController == null)
                    mainController = new MainController();
                return mainController;
            }
        }

        public App()
        {
            this.Resources.Add("mainController", MainController);
            this.Resources.Add("mainDataLoader", MainController.DataLoader);
            this.Resources.Add("mainRefData", MainController.RefData);
            this.Resources.Add("mainData", MainController.Data);
            this.Resources.Add("mainTreeController", MainController.TreeController);
        }

        public DataLoader MainDataLoader
        {
            get { return MainController.DataLoader; }
        }

        public RefData MainRefData
        {
            get { return MainController.RefData; }
        }

        public Data MainData
        {
            get { return MainController.Data; }
        }

        public TreeController MainTreeController
        {
            get { return MainController.TreeController; }
        }
    }
}
