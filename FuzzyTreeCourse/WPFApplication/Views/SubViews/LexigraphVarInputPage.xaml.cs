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

        public LexigraphVarInputPage()
        {
            InitializeComponent();
        }
    }
}
