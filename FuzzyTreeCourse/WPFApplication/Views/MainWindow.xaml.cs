using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FuzzyTreeWPF.Views;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FuzzyTreeLib.Models.Counters;
using WPFApplication.Helpers;
using WPFApplication.Views;

namespace FuzzyTreeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Opens another page - Lexigraphics Settuper Page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseLexigraphicsValues(object sender, RoutedEventArgs e)
        {
            // going to another page - lexigraphics browser page
            LexigraphicsBrowser lexigraphicsBrowserPage = new LexigraphicsBrowser();
            lexigraphicsBrowserPage.Show();
        }

        /// <summary>
        /// Opens new window with formed fuzzy tree to watch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WatchFuzzyTree(object sender, RoutedEventArgs e)
        {
            var mainController = ResourcePicker.GetMainController();

            if (!mainController.TreeController.IsConstructed)
                MessageBox.Show("Firstly build a tree by clicking 'Find Result' in 'Browse Lexigraphics Values' page.");
            else
            {
                (new FuzzyTreePage()).Show();
            }
        }

        /// <summary>
        /// Opens new page with short description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoToOtherInfo(object sender, RoutedEventArgs e)
        {
            (new InstructionsPage()).Show();
        }
    }
}
