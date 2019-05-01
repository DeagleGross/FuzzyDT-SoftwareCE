using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyTreeLib.Models
{
    /// <summary>
    /// Has "Name" ("Проживание в регионе")
    /// Has list of double vals ("3,4,5,6,7,8, etc")
    /// </summary>
    public class Atribut
    {
        public string Name { get; set; }
        public List<double> Values { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Atribut()
        {
            this.Values = new List<double>();
        }

        /// <summary>
        /// Constructor initializing Name
        /// </summary>
        /// <param name="Name"></param> 
        public Atribut(string Name)
        {
            this.Name = Name;
            this.Values = new List<double>();
        }

        public void LoadDoubleList(List<double> vals)
        {
            Values.AddRange(vals);
        }

        /// <summary>
        /// Normalizes values by its max value.
        /// So all values are in [0;1] interval
        /// </summary>
        public void NormalizeValues()
        {
            double maxValue = Values.Max();

            for (int i = 0; i < Values.Count; ++i)
                this.Values[i] /= maxValue;
        }
    }
}
