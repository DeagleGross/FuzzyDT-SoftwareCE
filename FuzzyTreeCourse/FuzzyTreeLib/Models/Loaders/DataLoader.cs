using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.Counters;
using FuzzyTreeLib.Models.DataSetObjects;
using FuzzyTreeLib.Models.Loaders;
using FuzzyTreeLib.Models.Ref;

namespace FuzzyTreeLib.Models
{
    public class DataLoader
    {
        const string Path = "../../../DataSets/kaggleCOCOMO.csv";
        public List<InputValue> InputValues { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public DataLoader()
        {
            this.InputValues = new List<InputValue>();
        }

        private void AddInputValueAndGetItFromConsole(string name)
        {
            InputValue inputValue = new InputValue(name);
            inputValue.AskAndFill();

            InputValues.Add(inputValue);
        }

        /// <summary>
        /// Add to inputVals container value passed from GUI
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        private void AddInputFromGUI(string name, double value)
        {
            InputValue inputValue = new InputValue(name);
            inputValue.Value = value;

            InputValues.Add(inputValue);
        }

        /// <summary>
        /// Empties container of inputVals. Used before filling container
        /// </summary>
        public void EmptyInputsBeforeFill() => InputValues.Clear();

        public void FillInputsFromGUI(string name, double value)
        {
            AddInputFromGUI(name, value);
        }

        /// <summary>
        /// Empty inputVals container and fill it with console
        /// </summary>
        /// <param name="atributs"></param>
        public void FillInputs(List<Atribut> atributs)
        {
            // to empty input
            InputValues.Clear();

            for (int i = 0; i < atributs.Count-1; ++i)
            {
                this.AddInputValueAndGetItFromConsole(atributs[i].Name);
            }
        }

        /// <summary>
        /// Constructor initializing path to file
        /// </summary>
        /// <param name="path"> string path </param>
        public DataLoader(string path)
        {
            this.InputValues = new List<InputValue>();
        }

        public Atribut LoadAtributThroughList(string name, List<double> vals)
        {
            Atribut atr = new Atribut(name);
            atr.LoadDoubleList(vals);

            return atr;
        }

        /// <summary>
        /// Loads Sample test to passed controller
        /// for CONSOLE-VERSION.
        /// </summary>
        /// <param name="controllerMain"></param>
        public void LoadSampleTest(MainController controllerMain)
        {
            // ADDING TEST SAMPLE ON TREE
            // --------------------------
            string name;
            List<double> dataDoubles;
            List<RefLexigraphic> lexigraphics;

            // ДОХОД
            name = "Доход";
            dataDoubles = new List<double>() { 10000, 15000, 20000,
                30000, 25000, 35000, 50000 };
            //list containing lexVars to atribut
            lexigraphics = new List<RefLexigraphic>();
            lexigraphics.Add(new RefLexigraphic("Малый", 0, 0, 8000, 21000));
            lexigraphics.Add(new RefLexigraphic("Средний", 8000, 21000, 25000, 38000));
            lexigraphics.Add(new RefLexigraphic("Высокий", 25000, 38000, double.MaxValue, double.MaxValue));

            controllerMain.LoadAtributData_and_RefAtributData(
                name, dataDoubles, lexigraphics
            );

            // ПРОЖИВАНИЕ В РЕГИОНЕ
            name = "Проживание в регионе";
            dataDoubles = new List<double>() { 0, 10, 15, 20, 35, 40, 40 };
            //list containing lexVars to atribut
            lexigraphics = new List<RefLexigraphic>();
            lexigraphics.Add(new RefLexigraphic("Временно", 0, 0, 8, 21));
            lexigraphics.Add(new RefLexigraphic("Продолжительно", 8, 21, 24, 38));
            lexigraphics.Add(new RefLexigraphic("Постоянно", 24, 38, double.MaxValue, double.MaxValue));

            controllerMain.LoadAtributData_and_RefAtributData(
                name, dataDoubles, lexigraphics
            );

            // РЕЙТИНГ
            controllerMain.LoadAtributThroughDataLoader(
                //"Рейтинг", new List<double>() { 0.0, 0.0, 0.1, 0.3, 0.7, 0.9, 1.0 }
                "Рейтинг", new List<double>() { 0, 0, 10, 30, 70, 90, 100 }
            );

            //
            // ### ADDING TEST SAMPLE ON TREE
        }

        /// <summary>
        /// Loads sample input of DATA to controller.
        /// RefData is filled using GUI (also fillInputValues button)
        /// </summary>
        /// <param name="mainController"> controller which data to fill </param>
        public void LoadSampleTestForGUI(MainController mainController)
        {
            mainController.Data.DataTable = new List<Atribut>();

            // доход
            mainController.LoadAtributThroughDataLoader("Доход", 
                new List<double>() { 10000, 15000, 20000, 30000, 25000, 35000, 50000 });

            // проживание в регионе
            mainController.LoadAtributThroughDataLoader("Проживание в регионе", 
                new List<double>() { 0, 10, 15, 20, 35, 40, 40 });

            // результативный массив значений - рейтинг (%)
            mainController.LoadAtributThroughDataLoader(
                "Рейтинг", new List<double>() { 0, 0, 10, 30, 70, 90, 100 }
            );
        }

        /// <summary>
        /// Returns List of Cocomo objects from CSV file
        /// </summary>
        /// <returns></returns>
        private List<CocomoObject> GetCocomosFromCSV()
        {

            /*
             0   1      2         3         4        5     6        7           8            9            10         11        12
             id,Project,TeamExp,ManagerExp,YearEnd,Length,Effort,Transactions,Entities,PointsNonAdjust,Adjustment,PointsAjust,Language
             */
            List<CocomoObject> cocomos = new List<CocomoObject>();
            int tmp;

            using (var reader = new StreamReader(Path))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (!int.TryParse(values[0], out tmp))
                        continue;

                    cocomos.Add(new CocomoObject()
                    {
                        TeamExp = int.Parse(values[2]),
                        ManagerExp = int.Parse(values[3]),
                        Length = int.Parse(values[5]),
                        Effort = int.Parse(values[6]),
                        Entities = int.Parse(values[8])
                    });
                }
            }

            return cocomos;
        }

        /// <summary>
        /// Returns list of double to load to data.
        /// </summary>
        /// <param name="cocomos"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private List<double> GetDataFactorToLoad(List<CocomoObject> cocomos, string parameter)
        {
            List<double> res = new List<double>();

            switch (parameter)
            {
                case "TeamExp":
                {
                    foreach (var cocomo in cocomos)
                    {
                        if (cocomo.TeamExp < 0)
                            cocomo.TeamExp = 0;
                        res.Add(cocomo.TeamExp);
                    }
                        
                    break;
                }
                case "ManagerExp":
                {
                    foreach (var cocomo in cocomos)
                    {
                        if (cocomo.ManagerExp < 0)
                            cocomo.ManagerExp = 0;
                        res.Add(cocomo.ManagerExp);
                    }
                    break;
                }
                case "Length":
                {
                    foreach (var cocomo in cocomos)
                    {
                        if (cocomo.Length < 0)
                            cocomo.Length = 0;
                        res.Add(cocomo.Length);
                    }
                        
                    break;
                }
                case "Entities":
                {
                    foreach (var cocomo in cocomos)
                        res.Add(cocomo.Entities);
                    break;
                }
                case "Effort":
                {
                    foreach (var cocomo in cocomos)
                        res.Add(cocomo.Effort);
                    break;
                }
                default:
                    throw new ArgumentException();
                    break;
            }

            return res;
        }

        /// <summary>
        /// Uses const path to folder 'DataSets' to load data.
        /// Fills Controller.Data for further actions
        /// </summary>
        /// <param name="mainController"></param>
        public void LoadCocomoDataSet(MainController mainController)
        {
            List<CocomoObject> cocomos = GetCocomosFromCSV();

            mainController.Data.DataTable = new List<Atribut>();

            // Team Experience
            mainController.LoadAtributThroughDataLoader("Team Experience",
                GetDataFactorToLoad(cocomos, "TeamExp"));

            // Manager Experience
            mainController.LoadAtributThroughDataLoader("Manager Experience",
                GetDataFactorToLoad(cocomos, "ManagerExp"));

            // Length
            mainController.LoadAtributThroughDataLoader("Length",
                GetDataFactorToLoad(cocomos, "Length"));

            // Entities
            mainController.LoadAtributThroughDataLoader("Entities",
                GetDataFactorToLoad(cocomos, "Entities"));

            // Effort
            // result array of values
            mainController.LoadAtributThroughDataLoader("Effort", 
                GetDataFactorToLoad(cocomos, "Effort"));

            // need to save max value of efforts for de-normalization
            mainController.Data.SaveMaxResultDouble(GetDataFactorToLoad(cocomos, "Effort"));
        }
    }
}
