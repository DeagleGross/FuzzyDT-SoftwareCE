using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyTreeLib.Models.DataSetObjects
{
    class CocomoObject
    {
        /// <summary>
        /// year-experience of team
        /// </summary>
        public int TeamExp { get; set; }
        /// <summary>
        /// year-experience of product-manager
        /// </summary>
        public int ManagerExp { get; set; }
        /// <summary>
        /// months needed to create product
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// amount of entities
        /// </summary>
        public int Entities { get; set; }

        /// <summary>
        /// Used as result parameter in COCOMO-model
        /// </summary>
        public int Effort { get; set; }
    }
}
