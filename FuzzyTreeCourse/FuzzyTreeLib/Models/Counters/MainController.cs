﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.Ref;
using FuzzyTreeLib.Models.Tree;

namespace FuzzyTreeLib.Models.Counters
{
    public class MainController
    {
        public Data Data { get; set; }
        public RefData RefData { get; set; }
        public DataLoader DataLoader { get; set; }
        public TreeController TreeController { get; set; }

        /// <summary>
        /// because last atribut is results - we have to get not all
        /// </summary>
        private int AmountOfAtributs => Data.GetAmountOfNonResultAtributs;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainController()
        {
            Data = new Data();
            RefData = new RefData();
            DataLoader = new DataLoader();
            TreeController = new TreeController();
        }

        /// <summary>
        /// Constructor with number of decimals for refData
        /// </summary>
        public MainController(int numberOfDecimals)
        {
            Data = new Data();
            RefData = new RefData(numberOfDecimals);
            DataLoader = new DataLoader();
            TreeController = new TreeController();
        }

        public void LoadAtributThroughDataLoader(string name, List<double> valsDoubles)
        {
            Data.DataTable.Add(this.DataLoader.LoadAtributThroughList(name, valsDoubles));
        }

        public void LoadAtributData_and_RefAtributData(string name, List<double> valsDoubles, List<RefLexigraphic> lexigraphics)
        {
            LoadAtributThroughDataLoader(name, valsDoubles);
            RefData.LoadRefAtribut(name, lexigraphics);
        }

        /// <summary>
        /// Loads new Atribut object to Data
        /// </summary>
        /// <param name="atr">object to load in Data List</param>
        public void LoadAtributToData(Atribut atr)
        {
            Data.DataTable.Add(atr);
        }

        /// <summary>
        /// Loads list of Atribut objects to Data
        /// </summary>
        /// <param name="atrs">list of objects to load</param>
        public void LoadAtributsToData(List<Atribut> atrs)
        {
            Data.DataTable.AddRange(atrs);
        }

        /// <summary>
        /// reforms data values that are done in a table form
        /// to all FORMED ALREADY reference objects
        /// </summary>
        public void CountReferenceValuesFromDataValues()
        {
            for (int i = 0; i < this.AmountOfAtributs; ++i)
            {
                foreach (RefLexigraphic refLexigraphic in this.RefData.RefTable[i].Atribut)
                {
                    refLexigraphic.ReformDataValsToReferenceValues(this.Data.DataTable[i].Values);
                }
            }
        }

        /// <summary>
        /// After all data values were loaded and reformed to reference
        /// entropy can be counted for all reference vals and refAtributs
        /// </summary>
        public void CountEntropyForAllLexigraphics()
        {
            // firstly getting list of doubles - RESULT ones
            // and counting entropy - shared for RefData
            List<double> resultDoubles = Data.DataTable[Data.DataTable.Count - 1].Values;
            this.RefData.CountEntropy(resultDoubles);

            for (int i = 0; i < this.AmountOfAtributs; ++i)
            {
                foreach (RefAtribut refAtribut in this.RefData.RefTable)
                {
                    foreach (RefLexigraphic refLexigraphic in refAtribut.Atribut)
                    {
                        refLexigraphic.CountAndSetupEntropy(resultDoubles);
                    }
                }
            }
        }

        /// <summary>
        /// Counts infoGrowth for lexigraphics
        /// </summary>
        public void CountGrowthFunctionForAllLexigraphics()
        {
            this.RefData.CountGrowthFunctionForAllLexigraphics();
        }

        /// <summary>
        /// Normalization of Result Values in Data object
        /// </summary>
        public void NormalizeResultValues() => Data.NormalizeResultDoubles();

        /// <summary>
        /// Returns result of tree implementation
        /// </summary>
        /// <returns></returns>
        public double GetDoubleResultValue()
        {
            // Construct Tree
            TreeController.ConstructTree(RefData.RefTable, true);

            // Get Input Vals
            WriteInput();

            // Return result
            return TreeController.GetTheResult(DataLoader.InputValues);
        }

        /// <summary>
        /// Write Input using Console
        /// </summary>
        public void WriteInput()
        {
            DataLoader.FillInputs(Data.DataTable);
        }

        /// <summary>
        /// Returns string impresentation of All RefAtributs with field values
        /// and last Data column - result values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string str = "";

            // printing resulting values to see if are they normalized correctly
            str += "Result values::\n";
            int k = 0;
            foreach (var doubleVal in Data.GetResultDoubles)
                str += "\t" + k++ + ") " + doubleVal + ";\n";

            str += this.RefData.ToString();

            return str;
        }
    }
}
