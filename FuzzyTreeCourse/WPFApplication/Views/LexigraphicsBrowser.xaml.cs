using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FuzzyTreeLib.Models.Counters;

namespace FuzzyTreeWPF.Views
{
    /// <summary>
    /// Interaction logic for LexigraphicsBrowser.xaml
    /// </summary>
    public partial class LexigraphicsBrowser : Window
    {
        public LexigraphicsBrowser()
        {
            InitializeComponent();
        }

        private void CountResult(object sender, RoutedEventArgs e)
        {
            MainController mainController = (MainController)Application.Current.Resources["mainController"];

            MessageBox.Show(mainController.ToString());
        }
    }
}
