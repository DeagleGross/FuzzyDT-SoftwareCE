using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.Ref;

namespace FuzzyTreeLib.Models
{
    /// <summary>
    /// Table containing all REFERENCE atributs
    /// </summary>
    public class RefData
    {
        public List<RefAtribut> RefTable { get; set; }
        private int NumberOfDecimals { get; }
        public double Entropy { get; set; }

        /// <summary>
        /// constructor by default
        /// </summary>
        public RefData()
        {
            RefTable = new List<RefAtribut>();
        }

        /// <summary>
        /// Constructor setting number of decimals after ","
        /// </summary>
        /// <param name="numberOfDecimals">number of decimals after ","</param>
        public RefData(int numberOfDecimals = 2)
        {
            RefTable = new List<RefAtribut>();
            this.NumberOfDecimals = numberOfDecimals;
        }

        /// <summary>
        /// Add new instance of RefAtribut to table.
        /// RefAtribut is a class containing list of lexigraphics
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lexigraphics"></param>
        public void LoadRefAtribut(string name, List<RefLexigraphic> lexigraphics)
        {
            RefTable.Add(new RefAtribut(name, lexigraphics, NumberOfDecimals));
        }

        /// <summary>
        /// returns result of EntropyFunction
        /// f() = - pos/sum * log(pos/sum, 2) - neg/sum * log(neg/sum, 2)
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="neg"></param>
        /// <returns></returns>
        private double GetEntrFuncRes(double pos, double neg)
        {
            double sum = pos + neg;

            double posDiv = pos / sum;
            double negDiv = neg / sum;

            double result = (-1) * posDiv * (Math.Log(posDiv, 2));
            result += (-1) * negDiv * (Math.Log(negDiv, 2));

            return result;
        }

        /// <summary>
        /// Counts the positive value of  reference (entropy)
        /// as summ of min(referenceVal, DataVal)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private double GetPosRef(List<double> values)
        {
            double pos = 0;

            for (int i = 0; i < values.Count; i++)
                pos += values[i];

            return pos;
        }

        /// <summary>
        /// Counts the negative value of  reference (entropy)
        /// as summ of min(referenceVal, 1-DataVal)
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private double GetNegRef(List<double> values)
        {
            double neg = 0;

            for (int i = 0; i < values.Count; i++)
                neg += (1 - values[i]);

            return neg;
        }

        /// <summary>
        /// setups SHARED entropy for whole DB
        /// </summary>
        /// <param name="resultDoubles"></param>
        public void CountEntropy(List<double> resultDoubles)
        {
            double pos = this.GetPosRef(resultDoubles);
            double neg = this.GetNegRef(resultDoubles);

            this.Entropy = Math.Round(GetEntrFuncRes(pos, neg), NumberOfDecimals);
        }

        public void CountGrowthFunctionForAllLexigraphics()
        {
            foreach (var refAtribut in RefTable)
            {
                foreach (var refLexigraphic in refAtribut.Atribut)
                {
                    refLexigraphic.CountAndSetupInformationGrowth(Entropy);
                }
            }
        }

        public override string ToString()
        {
            string str = "";

            str += "Reference Data:: \n";
            str += "  Entropy: " + this.Entropy + "\\\\\n";

            foreach (RefAtribut refAtribut in RefTable)
            {
                str += refAtribut.ToString();
            }

            return str;
        }
    }
}
