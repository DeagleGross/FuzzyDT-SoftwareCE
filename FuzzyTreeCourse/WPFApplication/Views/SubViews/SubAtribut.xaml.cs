﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
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

        public event EventHandler UserControlClicked;
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
