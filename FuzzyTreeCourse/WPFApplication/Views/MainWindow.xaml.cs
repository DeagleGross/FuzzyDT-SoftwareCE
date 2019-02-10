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
            MessageBox.Show("Going to lexigraphics!");

            // going to another page - lexigraphics browser page
            LexigraphicsBrowser lexigraphicsBrowserPage = new LexigraphicsBrowser();
            lexigraphicsBrowserPage.Show();
        }


        private void WatchFuzzyTree(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Going to fuzzy tree!");
        }

        private void GoToOtherInfo(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Going to others!");
        }
    }
}
