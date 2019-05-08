using System;
using System.Windows;
using System.Windows.Controls;

namespace FuzzyTreeWPF.Views.SubViews
{
    /// <summary>
    /// Interaction logic for SubAtribut.xaml
    /// </summary>
    public partial class SubAtribut : UserControl
    {
        /// <summary>
        /// Atribut Name
        /// </summary>
        public static readonly DependencyProperty AtributNameProperty =
            DependencyProperty.Register("AtributName", typeof(string), typeof(SubAtribut), new PropertyMetadata(null));

        /// <summary>
        /// From Property
        /// </summary>
        public static readonly DependencyProperty FromProperty =
            DependencyProperty.Register("From", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));

        /// <summary>
        /// Left Property
        /// </summary>
        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));

        /// <summary>
        /// Right Property
        /// </summary>
        public static readonly DependencyProperty RightProperty =
            DependencyProperty.Register("Right", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));

        /// <summary>
        /// To Property
        /// </summary>
        public static readonly DependencyProperty ToProperty =
            DependencyProperty.Register("To", typeof(double), typeof(SubAtribut), new PropertyMetadata(null));

        public string AtributName
        {
            get => (string)GetValue(AtributNameProperty);
            set => SetValue(AtributNameProperty, value);
        }

        public double From
        {
            get => (double) GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public double Left
        {
            get => (double)GetValue(LeftProperty);
            set => SetValue(LeftProperty, value);
        }

        public double Right
        {
            get => (double)GetValue(RightProperty);
            set => SetValue(RightProperty, value);
        }

        public double To
        {
            get => (double)GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public SubAtribut()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns true if no values are placed incorrectly in order (left <= from <= to <= right)
        /// </summary>
        /// <returns></returns>
        public bool AreValuesCorrect()
        {
            if (Left > From || Left < 0) return false;
            if (From > To || From < 0) return false;
            if (To > Right || To < 0) return false;
            if (Right < 0) return false;

            return true;
        }

        
        public event EventHandler UserControlClicked;
        /// <summary>
        /// Occurs when text-value of textBoxes has changed and graph has to be redrawn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyValue_Changed(object sender, RoutedEventArgs e)
        {
            if (UserControlClicked != null)
            {
                UserControlClicked(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Standart overload of ToString for debug
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{AtributName}::from={From}, left={Left}, right={Right}, to={To}";
        }
    }
}
