using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyTreeLib.Models.Ref
{
    /// <summary>
    /// a whole block like "Проживание в регионе"
    /// with all lexigraphic variables in it with already found values
    /// </summary>
    public class RefAtribut
    {
        public string Name { get; set; }
        public List<RefLexigraphic> Atribut { get; set; }
        private int NumberOfDecimals { get; }

        public double InfoGrowth
        {
            get
            {
                double infoGrowth = 0;

                foreach (RefLexigraphic refLexigraphic in Atribut)
                    infoGrowth += refLexigraphic.InfoGrowth;

                return infoGrowth;
            }
        }

        /// <summary>
        /// default constructor
        /// </summary>
        public RefAtribut() { }

        /// <summary>
        /// Constructor setting name
        /// </summary>
        /// <param name="name"></param>
        public RefAtribut(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// constructor setting name and list of reference lexigraphic vars
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lexigraphics"></param>
        public RefAtribut(string name, List<RefLexigraphic> lexigraphics)
        {
            this.Name = name;
            this.Atribut = lexigraphics;
        }

        /// <summary>
        /// constructor setting name and list of reference lexigraphic vars
        /// also setups number of decimals after ','
        /// </summary>
        /// <param name="name">name of atribut</param>
        /// <param name="lexigraphics">list of RefLexigraphic variables to setup field</param>
        /// <param name="numberOfDecimals">number of decimals after ','</param>
        public RefAtribut(string name, List<RefLexigraphic> lexigraphics, int numberOfDecimals)
        {
            this.Name = name;
            this.NumberOfDecimals = numberOfDecimals;

            foreach (RefLexigraphic lexigraphic in lexigraphics)
                lexigraphic.SetNumberOfDecimals(this.NumberOfDecimals);
            this.Atribut = lexigraphics;
        }

        /// <summary>
        /// constructor that
        /// 1) setups name for RefAtribut
        /// 2) setups list of ReferenceLexigraphics
        /// 3) reloads values to every refLexigraphics according to
        ///    reference state of this atribut-lexigraphic    
        /// </summary>
        public RefAtribut(string name, List<double> values, List<RefLexigraphic> refLexigraphics)
        {
            this.Name = name;
            Atribut = refLexigraphics;

            foreach (RefLexigraphic refLexigraphic in refLexigraphics)
                refLexigraphic.ReformDataValsToReferenceValues(values);
        }

        /// <summary>
        /// adds new object of refLexigraphic
        /// </summary>
        /// <param name="refLexigraphic"></param>
        public void AddRefLexigraphic(RefLexigraphic refLexigraphic)
        {
            Atribut.Add(refLexigraphic);
        }

        public override string ToString()
        {
            string str = "";

            str += "\t" + Name + "::\n";
            str += "\tGrowth: " + this.InfoGrowth + "::\n";

            foreach (RefLexigraphic refLexigraphic in Atribut)
            {
                str += refLexigraphic.ToString();
            }

            return str;
        }
    }
}
