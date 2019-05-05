using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Instrumentation;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using FuzzyTreeLib.Models.Tree;
using Shields.GraphViz.Components;
using Shields.GraphViz.Models;
using Shields.GraphViz.Services;
using WPFApplication.Helpers;

namespace WPFApplication.Views
{
    /// <summary>
    /// Interaction logic for FuzzyTreePage.xaml
    /// </summary>
    public partial class FuzzyTreePage : Window
    {
        public FuzzyTreePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Calls to MainController object from resource-dictionary and constructs a graph from tree-controller.
        /// </summary>
        /// <returns></returns>
        private HashSet<KeyValuePair<string, string>> GetEdgesFromFuzzyTree()
        {
            var mainController = ResourcePicker.GetMainController();

            TreeController tree = mainController.TreeController;
            HashSet<KeyValuePair<string, string>> edges = tree.GetTreeEdgeStatements();

            return edges;
        }

        /// <summary>
        /// Renders fuzzy-tree to png pic and saves it by path
        /// </summary>
        private async void RenderGraphToPng()
        {
            if (!ResourcePicker.GetMainController().TreeController.IsConstructed)
            {
                MessageBox.Show("Tree is not constructed. Please build it at first");
                return;
            }

            var GraphVizBinPath = "../../../GraphVizPackage/bin";
            var GraphPicsPath = "../../GraphPics/graph.png";

            // getting edges of tree in list of pairs (name A -> name B)
            var edges = GetEdgesFromFuzzyTree();

            var edgeStatements = new List<EdgeStatement>();

            // filling graph structure with edges
            foreach (var keyValuePair in edges)
                edgeStatements.Add(EdgeStatement.For(keyValuePair.Key, keyValuePair.Value));

            Graph graph = Graph.Directed.AddRange(edgeStatements);

            IRenderer renderer = new Renderer(GraphVizBinPath);
            using (Stream file = File.Open(GraphPicsPath, FileMode.OpenOrCreate))
            {
                await renderer.RunAsync(
                    graph, file,
                    RendererLayouts.Dot,
                    RendererFormats.Png,
                    CancellationToken.None);
            }

            //mainFuzzyTreeGraphImage.Source = new BitmapImage(new Uri(GraphPicsPath, UriKind.Relative));
        }

        /// <summary>
        /// Occurs when page loads for rendering picture for fuzzy-tree-implementation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuzzyTreePage_OnLoaded(object sender, RoutedEventArgs e)
        {
            RenderGraphToPng();
        }

        /// <summary>
        /// Occurs when button to refresh fuzzy-tree-graph is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRefreshFuzzyTreeGraph(object sender, RoutedEventArgs e)
        {
            RenderGraphToPng();
        }
    }
}
