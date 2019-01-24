using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.Counters;
using FuzzyTreeLib.Models.Ref;

namespace FuzzyTreeCourse
{
    class Program
    {
        private static MainController _controllerMain;
        private const int NumberOfDecimals = 2;

        private static void ViewConsole() { Console.ReadLine(); }

        static void Main(string[] args)
        {
            _controllerMain = new MainController(NumberOfDecimals);
                
            // loading sample from site 
            _controllerMain.DataLoader.LoadSampleTest(_controllerMain);

            // Normalizing resultValues in Data
            _controllerMain.NormalizeResultValues();

            // Reforming values
            _controllerMain.CountReferenceValuesFromDataValues();

            _controllerMain.CountEntropyForAllLexigraphics();

            _controllerMain.CountGrowthFunctionForAllLexigraphics();

            Console.WriteLine(_controllerMain.ToString());

            // Counting result
            /* All needed done inside ResultCounter class:
             * 1) construct a tree
             * 2) get input
             * 3) count results
             */
            do
            {
                Console.WriteLine($"Percentage of receiving credit: {_controllerMain.GetDoubleResultValue()*100:F2}%");
                Console.WriteLine("Press ESC to quit. Any other key to repeat input.");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
            

            ViewConsole();
        }
    }
}
