using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyTreeLib.Models
{
    /// <summary>
    /// Contains list of Atributs - data that was received from user
    /// </summary>
    public class Data
    {
        public List<Atribut> DataTable { get; set; }

        public Data()
        {
            DataTable = new List<Atribut>();
        }

        /// <summary>
        /// returns list of doubles that contains resulting values
        /// - what we are searching in the task
        /// </summary>
        public List<double> GetResultDoubles => DataTable[DataTable.Count - 1].Values;

        /// <summary>
        /// Returns count of all atributs except resulting one
        /// </summary>
        public int GetAmountOfNonResultAtributs => DataTable.Count - 1;

        /// <summary>
        /// Normalizes values in resultAtribut
        /// </summary>
        public void NormalizeResultDoubles()
        {
            DataTable[DataTable.Count-1].NormalizeValues();
        }
    }
}
