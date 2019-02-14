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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuzzyTreeWPF.Views.SubViews
{
    /// <summary>
    /// Interaction logic for LexigraphVarInputPage.xaml
    /// </summary>
    public partial class LexigraphVarInputPage : UserControl
    {
        List<SubViews.SubAtribut> subAtributs;

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(LexigraphVarInputPage), new PropertyMetadata(null));
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        /// <summary>
        /// Adds all UI elements "SubAtribut" to list
        /// </summary>
        private void FormSubAtributsUIelemsList()
        {
            subAtributs = new List<SubAtribut>
            {
                SubAtribut1,
                SubAtribut2,
                SubAtribut3,
                SubAtribut4,
                SubAtribut5,
                SubAtribut6,
                SubAtribut7,
                SubAtribut8,
                SubAtribut9
            };
        }

        public LexigraphVarInputPage()
        {
            InitializeComponent();
        }

        private void AmountOfAtributsPicker_OnDropDownClosed(object sender, EventArgs e)
        {
            if (subAtributs == null)                
                FormSubAtributsUIelemsList();

            int indexSelected = CmbBoxAtributsQuantity.SelectedIndex + 3;

            for (int i = 0; i < indexSelected; i++)
                subAtributs[i].Visibility = Visibility.Visible;
                
            for (int i = indexSelected; i < subAtributs.Count; i++)
                subAtributs[i].Visibility = Visibility.Collapsed;
        }
    }
}
