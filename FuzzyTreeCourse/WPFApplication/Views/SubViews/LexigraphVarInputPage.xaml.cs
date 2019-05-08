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
using FuzzyTreeLib.Models.Counters;
using FuzzyTreeLib.Models.Ref;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using WPFApplication.Helpers;
using ChartPoint = LiveCharts.ChartPoint;

namespace FuzzyTreeWPF.Views.SubViews
{
    /// <summary>
    /// Interaction logic for LexigraphVarInputPage.xaml
    /// </summary>
    public partial class LexigraphVarInputPage : UserControl
    {
        List<SubViews.SubAtribut> subAtributs;

        public List<SubViews.SubAtribut> SubAtributs
        {
            get
            {
                if (subAtributs == null)
                    FormSubAtributsUIelemsList();
                return subAtributs;
            }
        }

        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register("Caption", typeof(string), typeof(LexigraphVarInputPage), new PropertyMetadata(null));
        public string Caption
        {
            get => (string)GetValue(CaptionProperty);
            set => SetValue(CaptionProperty, value);
        }

        public static readonly DependencyProperty InputAtributValueProperty =
            DependencyProperty.Register("InputAtributValue", typeof(double), typeof(LexigraphVarInputPage), new PropertyMetadata(null));
        public double InputAtributValue
        {
            get => (double)GetValue(InputAtributValueProperty);
            set => SetValue(InputAtributValueProperty, value);
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
            if (subAtributs == null || subAtributs.Count == 0)                
                FormSubAtributsUIelemsList();

            int indexSelected = CmbBoxAtributsQuantity.SelectedIndex + 3;

            for (int i = 0; i < indexSelected; i++)
                subAtributs[i].Visibility = Visibility.Visible;
                
            for (int i = indexSelected; i < subAtributs.Count; i++)
                subAtributs[i].Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Setups mainController RefData with inputted params
        /// </summary>
        /// <param name="mainController"> mainController to be filled with reference data </param>
        public void FillInputData(MainController mainController)
        {
            /* TODO fill list<RefLexigraphic> and pass it to refData ! */

            // check on creation
            if (subAtributs == null || subAtributs.Count == 0)
                FormSubAtributsUIelemsList();

            List<RefLexigraphic> refLexigraphics = new List<RefLexigraphic>();

            foreach (var subAtr in this.subAtributs)
            {
                if (subAtr.IsVisible)
                {
                    refLexigraphics.Add(new RefLexigraphic
                    (
                        subAtr.AtributName, subAtr.Left, subAtr.From, subAtr.To, subAtr.Right
                    ));
                }
            }

            mainController.RefData.LoadRefAtribut(this.Caption, refLexigraphics);
        }

        /// <summary>
        /// References chart.series and adds all points to it
        /// </summary>
        private void InitializeChartSeries(IEnumerable<SubAtribut> subAtributs)
        {
            //Lets define a configuration by default for all the series in the SeriesCollection
            var config = new CartesianMapper<ChartPoint>()
                .X(chartPoint => chartPoint.XStart)
                .Y(chartPoint => chartPoint.YStart);

            Chart.Series = new SeriesCollection(config);

            foreach (var subAtr in subAtributs)
            {
                ChartValues<ChartPoint> chartPoints = new ChartValues<ChartPoint>();

                if (subAtr.Left != subAtr.From)
                    chartPoints.Add(new ChartPoint() { XStart = subAtr.Left, YStart = 0 });

                chartPoints.Add(new ChartPoint(){ XStart = subAtr.From, YStart = 1 });
                chartPoints.Add(new ChartPoint(){ XStart = subAtr.To, YStart = 1 });

                if (subAtr.To != subAtr.Right)
                    chartPoints.Add(new ChartPoint() { XStart = subAtr.Right, YStart = 0 });

                Chart.Series.Add(new LineSeries()
                {
                    Title = subAtr.AtributName,
                    Values = chartPoints,
                    LineSmoothness = 0
                });
            }
        }

        /// <summary>
        /// Called when any subView-subAtr value was changed.
        /// Calls event that redraws lexi-graph
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PropertyValue_Changed(object sender, EventArgs e)
        {
            // check on creation
            if (subAtributs == null || subAtributs.Count == 0)
                FormSubAtributsUIelemsList();

            InitializeChartSeries(subAtributs.Where(s => s.IsVisible));

            //Chart.Series = new SeriesCollection
            //{
            //    new LineSeries
            //    {
            //        Title = "Series 1",
            //        Values = new ChartValues<double> { 4, 6, 5, 2 ,4 },
            //        LineSmoothness = 0
            //    },
            //    new LineSeries
            //    {
            //        Title = "Series 2",
            //        Values = new ChartValues<double> { 6, 7, 3, 4 ,6 },
            //        PointGeometry = null,
            //    },
            //    new LineSeries
            //    {
            //        Title = "Series 3",
            //        Values = new ChartValues<double> { 4,2,7,2,7 },
            //        PointGeometry = DefaultGeometries.Square,
            //        PointGeometrySize = 15
            //    }
            //};
        }
    }
}
