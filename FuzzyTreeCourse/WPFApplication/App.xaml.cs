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
        private const int NumberOfDecimals = 2;
        private MainController mainController;

        /// <summary>
        /// Accessor to mainController
        /// </summary>
        public MainController MainController
        {
            get
            {
                if (mainController == null)
                    mainController = new MainController(NumberOfDecimals);
                return mainController;
            }
        }

        public App()
        {
            this.Resources.Add("constNumberOfDecimals", NumberOfDecimals);
            this.Resources.Add("mainController", MainController);
            this.Resources.Add("mainDataLoader", MainDataLoader);
            this.Resources.Add("mainRefData", MainRefData);
            this.Resources.Add("mainData", MainData);
            this.Resources.Add("mainTreeController", MainTreeController);
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
