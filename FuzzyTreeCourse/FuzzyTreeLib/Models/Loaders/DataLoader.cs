using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.Counters;
using FuzzyTreeLib.Models.Loaders;
using FuzzyTreeLib.Models.Ref;

namespace FuzzyTreeLib.Models
{
    public class DataLoader
    {
        private string _path;

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
            this._path = path;
            this.InputValues = new List<InputValue>();
        }

        public Atribut LoadAtributThroughList(string name, List<double> vals)
        {
            Atribut atr = new Atribut(name);
            atr.LoadDoubleList(vals);

            return atr;
        }

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
    }
}
