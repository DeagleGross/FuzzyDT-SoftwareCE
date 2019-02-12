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
    /// Interaction logic for SubAtribut.xaml
    /// </summary>
    public partial class SubAtribut : UserControl
    {
        public static readonly DependencyProperty AtributNameProperty =
            DependencyProperty.Register("AtributName", typeof(string), typeof(SubAtribut), new PropertyMetadata(null));
        public string AtributName
        {
            get { return (string)GetValue(AtributNameProperty); }
            set { SetValue(AtributNameProperty, value); }
        }

        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));
        public double From
        {
            get { return (double) GetValue(FromProperty); }
            set { SetValue(FromProperty, value); }
        }

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));
        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register("Right", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));
        public double Right
        {
            get { return (double)GetValue(RightProperty); }
            set { SetValue(RightProperty, value); }
        }

        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));
        public double To
        {
            get { return (double)GetValue(ToProperty); }
            set { SetValue(ToProperty, value); }
        }

        public SubAtribut()
        {
            InitializeComponent();
        }

        public override string ToString()
        {
            return $"{AtributName}::from={From}, left={Left}, right={Right}, to={To}";
        }
    }
}
