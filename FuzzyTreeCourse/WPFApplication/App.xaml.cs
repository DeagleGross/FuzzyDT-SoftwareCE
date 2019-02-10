using FuzzyTreeLib.Models.Counters;
using System.Windows;

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
    }
}
