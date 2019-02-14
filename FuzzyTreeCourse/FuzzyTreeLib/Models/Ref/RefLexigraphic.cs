using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.AbstractInterfaces;

namespace FuzzyTreeLib.Models.Ref
{
    /// <summary>
    /// has lexigraphic name for atribut
    /// ("Временно" for "Проживание в регионе")
    /// and list of double values for every lexighaph variable
    ///
    /// also HERE IS DONE convertment and counting % of reference
    /// having fields
    /// FROM - TO in double form
    /// </summary>
    public class RefLexigraphic : LexigraphAbstract
    {
        public List<double> Values { get; set; }

        // Number of decimals to round values
        private int NumberOfDecimals { get; set; }

        public void SetNumberOfDecimals(int numberOfDecimals)
        {
            NumberOfDecimals = numberOfDecimals;
        }

        // group of Reference Entropy fields
        public double Entropy { get; set; }
        public double InfoGrowth { get; set; }

        /* Some Functions to get fields from abstract class
         */
        public string GetName => Name;

        /// <summary>
        /// Returns array of 4 elems in such order: _left, _from, _to, _right
        /// </summary>
        /// <returns></returns>
        public double[] GetReferenceFunctionPoints()
        {
            double[] ret = new double[4];
            ret[0] = _left;
            ret[1] = _from;
            ret[2] = _to;
            ret[3] = _right;

            return ret;
        }

        public double GetPositiveRefValue => PositiveRef;
        public double GetNegativeRefValue => NegativeRef;


        /// <summary>
        /// default constructor
        /// </summary>
        public RefLexigraphic() { throw new ArgumentNullException(); }

        /// <summary>
        /// Constructor initializing states of
        /// REFERENCE lexigraphic properties
        /// Plus Name - how this lexigraphic is called
        /// </summary>
        /// <param name="name"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public RefLexigraphic(string name, double left, double from, double to, double right)
        {
            this.Name = name;

            this._from = from;
            this._to = to;

            this._left = left;
            this._right = right;
        }

        /// <summary>
        /// returns result of EntropyFunction
        /// f() = - pos/sum * log(pos/sum, 2) - neg/sum * log(neg/sum, 2)
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="neg"></param>
        /// <returns></returns>
        private double getEntrFuncRes(double pos, double neg)
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
                pos += (values[i] < this.Values[i]) ? values[i] : this.Values[i];

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
                neg += (1 - values[i] < this.Values[i]) ? 1 - values[i] : this.Values[i];

            return neg;
        }

        /// <summary>
        /// Returns value of general entropy using other private functions
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        private double CountEntropyForAtribut(List<double> values)
        {
            PositiveRef = Math.Round(GetPosRef(values), NumberOfDecimals);
            NegativeRef = Math.Round(GetNegRef(values), NumberOfDecimals);

            return Math.Round(getEntrFuncRes(PositiveRef, NegativeRef), NumberOfDecimals);
        }

        /// <summary>
        /// Reforms double Data Values to Reference ones
        /// </summary>
        /// <param name="values"> data values </param>
        public void ReformDataValsToReferenceValues(List<double> values)
        {
            Values = new List<double>();
            foreach (double val in values)
            {
                double reference = CountReference(val);
                reference = Math.Round(reference, NumberOfDecimals);
                Values.Add(reference);
            }
        }

        public void CountAndSetupInformationGrowth(double sharedEntropy)
        {
            this.InfoGrowth = sharedEntropy - this.Entropy;
        }

        /// <summary>
        /// Sets entropy for lexigraphic
        /// </summary>
        /// <param name="values">result values of data</param>
        public void CountAndSetupEntropy(List<double> values)
        {
            Entropy = CountEntropyForAtribut(values);
        }

        public override string ToString()
        {
            string str = "";

            str += "\t\t" + Name + "::\n";
            str += "\t\t  Entropy:    " + Entropy + "\\\\\n";
            str += "\t\t  InfoGrowth: " + InfoGrowth + "\\\\\n";

            for (int i = 0; i < Values.Count; ++i)
            {
                str += "\t\t\t" + i + ") " + Values[i] + ";\n";
            }

            return str;
        }
    }
}
