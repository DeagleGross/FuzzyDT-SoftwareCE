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
using FuzzyTreeWPF.Views.SubViews;

namespace WPFApplication.Views.SubViews
{
    /// <summary>
    /// Interaction logic for LexiGraph.xaml
    /// </summary>
    public partial class LexiGraph : UserControl
    {
        public LexiGraph()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Called when any property-field of lexigraphic was changed.
        /// </summary>
        /// <param name="subAtributs">Gets only really countable subAtrs - that are visible</param>
        public void GraphToReDraw(IEnumerable<SubAtribut> subAtributs)
        {
            // clearing children collection
            // not to stack all graph-implementations on it
            canGraph.Children.Clear();

            const double margin = 10;
            double xmin = margin;
            double xmax = canGraph.Width - margin;
            double ymin = margin;
            double ymax = canGraph.Height - margin;
            const double step = 10;

            double x_max_atr = 0;
            double x_len = xmax - xmin;

            foreach (var subAtribut in subAtributs)
            {
                if (subAtribut.Right != double.MaxValue)
                {
                    x_max_atr = Math.Max(x_max_atr, subAtribut.To);
                    x_max_atr = Math.Max(x_max_atr, subAtribut.Right);
                }
            }
                
            // Make the X axis.
            GeometryGroup xaxis_geom = new GeometryGroup();
            xaxis_geom.Children.Add(new LineGeometry(new Point(0, ymax), new Point(canGraph.Width, ymax)));

            Path xaxis_path = new Path();
            xaxis_path.StrokeThickness = 1;
            xaxis_path.Stroke = Brushes.Black;
            xaxis_path.Data = xaxis_geom;
            canGraph.Children.Add(xaxis_path);

            // Make the Y ayis.
            GeometryGroup yaxis_geom = new GeometryGroup();
            yaxis_geom.Children.Add(new LineGeometry(new Point(xmin, 0), new Point(xmin, canGraph.Height)));

            Path yaxis_path = new Path();
            yaxis_path.StrokeThickness = 1;
            yaxis_path.Stroke = Brushes.Black;
            yaxis_path.Data = yaxis_geom;
            canGraph.Children.Add(yaxis_path);

            // main data drawn
            Brush[] brushes = { Brushes.Red, Brushes.Green, Brushes.Blue, Brushes.Chocolate };
            Random rand = new Random();

            for (int data_set = 0; data_set < subAtributs.Count(); data_set++)
            {
                /*
                 left and right - 0
                 to and right - 1
                 */

                SubAtribut current = subAtributs.ElementAt(data_set);
                PointCollection points = new PointCollection();
                
                // placing points here

                // left
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(xmin + x_len * (current.Left / x_max_atr), ymax - margin / 2),
                    new Point(xmin + x_len * (current.Left / x_max_atr), ymax + margin / 2)));
                points.Add(new Point(xmin + x_len * (current.Left / x_max_atr), ymax));

                // from
                if (current.Left != current.From)
                {
                    xaxis_geom.Children.Add(new LineGeometry(
                        new Point(xmin + x_len * (current.From / x_max_atr), ymax - margin / 2),
                        new Point(xmin + x_len * (current.From / x_max_atr), ymax + margin / 2)));
                    points.Add(new Point(xmin + x_len * (current.From / x_max_atr), ymax / 2));
                }

                double x_property, y_val;

                // to
                x_property = (current.To == double.MaxValue) ? xmax : xmin + x_len * (current.To / x_max_atr);
                xaxis_geom.Children.Add(new LineGeometry(
                    new Point(x_property, ymax - margin / 2),
                    new Point(x_property, ymax + margin / 2)));
                points.Add(new Point(x_property, ymax/2));

                // right
                if (current.To != current.Right)
                {
                    y_val = (current.Right == double.MaxValue) ? ymax / 2 : ymax;
                    x_property = (current.Right == double.MaxValue) ? xmax : xmin + x_len * (current.Right / x_max_atr);
                    xaxis_geom.Children.Add(new LineGeometry(
                        new Point(x_property, ymax - margin / 2),
                        new Point(x_property, ymax + margin / 2)));
                    points.Add(new Point(x_property, y_val));
                }

                Polyline polyline = new Polyline();
                polyline.StrokeThickness = 1;
                polyline.Stroke = brushes[data_set];
                polyline.Points = points;

                canGraph.Children.Add(polyline);

                //int last_y = rand.Next((int)ymin, (int)ymax);

                //PointCollection points = new PointCollection();
                //for (double x = xmin; x <= xmax; x += step)
                //{
                //    last_y = rand.Next(last_y - 10, last_y + 10);
                //    if (last_y < ymin) last_y = (int)ymin;
                //    if (last_y > ymax) last_y = (int)ymax;
                //    points.Add(new Point(x, last_y));
                //}

                //Polyline polyline = new Polyline();
                //polyline.StrokeThickness = 1;
                //polyline.Stroke = brushes[data_set];
                //polyline.Points = points;

                //canGraph.Children.Add(polyline);
            }
        }
    }
}
