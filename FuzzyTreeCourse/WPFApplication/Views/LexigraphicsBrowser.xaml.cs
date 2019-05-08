using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using FuzzyTreeWPF.Views.SubViews;
using WPFApplication.Helpers;

namespace FuzzyTreeWPF.Views
{
    /// <summary>
    /// Interaction logic for LexigraphicsBrowser.xaml
    /// </summary>
    public partial class LexigraphicsBrowser : Window
    {
        List<SubViews.LexigraphVarInputPage> lexigraphVarInputPages;

        /// <summary>
        /// Method to return true when input values of properties of atribut are initialized properly
        /// </summary>
        /// <returns></returns>
        private bool ParsedAtributValuesCorrectly()
        {
            foreach (var lexInputPage in lexigraphVarInputPages)
            {
                foreach (var subAtr in lexInputPage.SubAtributs)
                {
                    if (!subAtr.AreValuesCorrect())
                    {
                        MessageBox.Show($"Error in subAttribute '{subAtr.AtributName}' of Attribute '{lexInputPage.Caption}'. " +
                                        $"There are some rules to follow: (Left <= From <= To <= Right)");
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Method to return false when inputValues of atributs are not initialized properly by user
        /// </summary>
        /// <returns></returns>
        private bool ParsedInputValuesCorrectly()
        {
            // parsing input vals of atributs
            foreach (var lexInputPage in this.lexigraphVarInputPages)
            {
                try
                {
                    if (lexInputPage.InputAtributValue.Equals(0))
                    {
                        MessageBox.Show($"Input value of '{lexInputPage.Caption}' atribut is empty. Can not do calculation.");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Input value of '{lexInputPage.Caption}' atribut can not be parsed to double. Can not do calculation.");
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Adds all UI elements "SubAtribut" to list
        /// </summary>
        private void FormLexigraphInputPagesList()
        {
            // TODO fill this list till end !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            lexigraphVarInputPages = new List<LexigraphVarInputPage>()
            {
                Atribut1,
                Atribut2,
                Atribut3,
                Atribut4
            };
        }

        public LexigraphicsBrowser()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Fills Data of main controller with input values of atributs
        /// And empties container of input values before
        /// </summary>
        private void FillRealInputOfAtributs()
        {
            var mainController = ResourcePicker.GetMainController();

            mainController.DataLoader.EmptyInputsBeforeFill();

            foreach (var lexInputPage in this.lexigraphVarInputPages)
            {
                mainController.DataLoader.FillInputsFromGUI(lexInputPage.Caption, lexInputPage.InputAtributValue);
            }
        }

        /// <summary>
        /// Main event to happen when counting result happens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountResult(object sender, RoutedEventArgs e)
        {
            // creating list if needed
            if (lexigraphVarInputPages == null || lexigraphVarInputPages.Count == 0)
                FormLexigraphInputPagesList();

            // TODO parse input - check all fields to be with value

            // escaping if input values are passed incorrectly
            if (!ParsedInputValuesCorrectly())
                return;

            // escaping if values of atributs are incorrect
            if (!ParsedAtributValuesCorrectly())
                return;

            // need to recreate maincontroller to destroy all RefData and tables that were created before
            var mainController = ResourcePicker.GetMainController(true);

            // filling data from DataLoader class
            //mainController.DataLoader.LoadSampleTestForGUI(mainController);
            // NOW USING COCOMO DATA SET
            mainController.DataLoader.LoadCocomoDataSet(mainController);

            foreach (var lexInputPage in this.lexigraphVarInputPages)
            {
                lexInputPage.FillInputData(mainController);
            }

            FillRealInputOfAtributs();

            // multiply by max value in result array because normalization was done before
            double result = mainController.CountTillEnd() * mainController.Data.MaxResultDouble;

            MessageBox.Show(
                result == 0
                    ? "Result Effort is 0.0 -> Check your input parameters, they can be out of bounds of SubAttributes values."
                    : $"Result Effort = {result:F2};");
        }

        /// <summary>
        /// Fills input text-boxes with sample values        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillSampleValues(object sender, RoutedEventArgs e)
        {
            // filling reference data
            FillFromCocomoDataSet();
            
            // filling data from DataLoader class - NOW USING COCOMO DATA SET
            ResourcePicker.GetMainController().DataLoader.LoadCocomoDataSet(ResourcePicker.GetMainController());
        }

        /// <summary>
        /// Fills Atrs as cocomo factored
        /// </summary>
        private void FillFromCocomoDataSet()
        {
            // team experience - checked on DBs
            {
                Atribut1.Caption = "Team Experience";
                Atribut1.InputAtributValue = 2;

                Atribut1.SubAtribut1.AtributName = "Little";
                Atribut1.SubAtribut1.Left = 0;
                Atribut1.SubAtribut1.From = 0;
                Atribut1.SubAtribut1.To = 1;
                Atribut1.SubAtribut1.Right = 2;

                Atribut1.SubAtribut2.AtributName = "Normal";
                Atribut1.SubAtribut2.Left = 1;
                Atribut1.SubAtribut2.From = 2;
                Atribut1.SubAtribut2.To = 2;
                Atribut1.SubAtribut2.Right = 3;

                Atribut1.SubAtribut3.AtributName = "Big";
                Atribut1.SubAtribut3.Left = 2;
                Atribut1.SubAtribut3.From = 3;
                Atribut1.SubAtribut3.To = 4;
                Atribut1.SubAtribut3.Right = 4;
            }

            // manager experience - checked on DB
            {
                Atribut2.Caption = "Manager Experience";
                Atribut2.InputAtributValue = 2;

                Atribut2.SubAtribut1.AtributName = "Small";
                Atribut2.SubAtribut1.Left = 0;
                Atribut2.SubAtribut1.From = 0;
                Atribut2.SubAtribut1.To = 1;
                Atribut2.SubAtribut1.Right = 2;

                Atribut2.SubAtribut2.AtributName = "Middle";
                Atribut2.SubAtribut2.Left = 1;
                Atribut2.SubAtribut2.From = 2;
                Atribut2.SubAtribut2.To = 3;
                Atribut2.SubAtribut2.Right = 4;

                Atribut2.SubAtribut3.AtributName = "High";
                Atribut2.SubAtribut3.Left = 3;
                Atribut2.SubAtribut3.From = 4;
                Atribut2.SubAtribut3.To = 5;
                Atribut2.SubAtribut3.Right = 7;
            }

            // length - checked on DB
            {
                Atribut3.Caption = "Project Length";
                Atribut3.InputAtributValue = 10;

                Atribut3.SubAtribut1.AtributName = "Short-time";
                Atribut3.SubAtribut1.Left = 0;
                Atribut3.SubAtribut1.From = 0;
                Atribut3.SubAtribut1.To = 4;
                Atribut3.SubAtribut1.Right = 5;

                Atribut3.SubAtribut2.AtributName = "Middle-time";
                Atribut3.SubAtribut2.Left = 4;
                Atribut3.SubAtribut2.From = 5;
                Atribut3.SubAtribut2.To = 15;
                Atribut3.SubAtribut2.Right = 20;

                Atribut3.SubAtribut3.AtributName = "Long-time";
                Atribut3.SubAtribut3.Left = 15;
                Atribut3.SubAtribut3.From = 20;
                Atribut3.SubAtribut3.To = 39;
                Atribut3.SubAtribut3.Right = 39;
            }

            // entities - checked on DB
            {
                Atribut4.Caption = "Entities";
                Atribut4.InputAtributValue = 250;

                Atribut4.SubAtribut1.AtributName = "Little amount";
                Atribut4.SubAtribut1.Left = 0;
                Atribut4.SubAtribut1.From = 5;
                Atribut4.SubAtribut1.To = 60;
                Atribut4.SubAtribut1.Right = 80;

                Atribut4.SubAtribut2.AtributName = "Natural amount";
                Atribut4.SubAtribut2.Left = 60;
                Atribut4.SubAtribut2.From = 80;
                Atribut4.SubAtribut2.To = 170;
                Atribut4.SubAtribut2.Right = 180;

                Atribut4.SubAtribut3.AtributName = "High amount";
                Atribut4.SubAtribut3.Left = 170;
                Atribut4.SubAtribut3.From = 180;
                Atribut4.SubAtribut3.To = 316;
                Atribut4.SubAtribut3.Right = 387;
            }
        }

        /// <summary>
        /// Occurs when page is loaded and filling of all gui input is needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LexigraphicsBrowser_OnLoaded(object sender, RoutedEventArgs e)
        {
            FillSampleValues(sender, e);
        }
    }
}
