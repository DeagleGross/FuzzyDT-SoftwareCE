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
                Atribut2
            };

            // TO HIDE LEXIGRAPH-VAR-INPUT-PAGE THAT ARE INVISIBLE

            //var tmp = new List<LexigraphVarInputPage>();

            //foreach (var lexigraphPage in lexigraphVarInputPages)
            //{
            //    if (lexigraphPage.IsVisible)
            //        tmp.Add(lexigraphPage);
            //}

            //lexigraphVarInputPages = tmp;

            // TO HIDE LEXIGRAPH-VAR-INPUT-PAGE THAT ARE INVISIBLE--------------------------
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

            // returning if input values are passed incorrectly
            if (!ParsedInputValuesCorrectly())
                return;

            // need to recreate maincontroller to destroy all RefData and tables that were created before
            var mainController = ResourcePicker.GetMainController(true);

            // filling data from DataLoader class
            mainController.DataLoader.LoadSampleTestForGUI(mainController);

            foreach (var lexInputPage in this.lexigraphVarInputPages)
            {
                lexInputPage.FillInputData(mainController);
            }

            FillRealInputOfAtributs();

            double result = mainController.CountTillEnd();

            MessageBox.Show($"result: % of credit = {result};");
        }

        /// <summary>
        /// Fills input text-boxes with sample values        
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FillSampleValues(object sender, RoutedEventArgs e)
        {
            /* TODO определить факторы для анализа */

            // filling reference data
            FillFromTestingSample();
            
            // filling data from DataLoader class
            ResourcePicker.GetMainController().DataLoader.LoadSampleTestForGUI(ResourcePicker.GetMainController());
        }

        /// <summary>
        /// Fills Atrs with testing for site
        /// Not real factors now
        /// </summary>
        private void FillFromTestingSample()
        {
            Atribut1.Caption = "Доход";
                Atribut1.SubAtribut1.AtributName = "Малый";
                Atribut1.SubAtribut1.Left = 0;
                Atribut1.SubAtribut1.From = 0;
                Atribut1.SubAtribut1.To = 8000;
                Atribut1.SubAtribut1.Right = 21000;

                Atribut1.SubAtribut2.AtributName = "Средний";
                Atribut1.SubAtribut2.Left = 8000;
                Atribut1.SubAtribut2.From = 21000;
                Atribut1.SubAtribut2.To = 25000;
                Atribut1.SubAtribut2.Right = 38000;

                Atribut1.SubAtribut3.AtributName = "Высокий";
                Atribut1.SubAtribut3.Left = 25000;
                Atribut1.SubAtribut3.From = 38000;
                Atribut1.SubAtribut3.To = double.MaxValue;
                Atribut1.SubAtribut3.Right = double.MaxValue;

            Atribut2.Caption = "Проживание в регионе";
                Atribut2.SubAtribut1.AtributName = "Временно";
                Atribut2.SubAtribut1.Left = 0;
                Atribut2.SubAtribut1.From = 0;
                Atribut2.SubAtribut1.To = 8;
                Atribut2.SubAtribut1.Right = 21;

                Atribut2.SubAtribut2.AtributName = "Продолжительно";
                Atribut2.SubAtribut2.Left = 8;
                Atribut2.SubAtribut2.From = 21;
                Atribut2.SubAtribut2.To = 24;
                Atribut2.SubAtribut2.Right = 38;

                Atribut2.SubAtribut3.AtributName = "Постоянно";
                Atribut2.SubAtribut3.Left = 24;
                Atribut2.SubAtribut3.From = 38;
                Atribut2.SubAtribut3.To = double.MaxValue;
                Atribut2.SubAtribut3.Right = double.MaxValue;
        }
    }
}
