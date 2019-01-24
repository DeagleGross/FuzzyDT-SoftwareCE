using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyTreeLib.Models.AbstractInterfaces
{
    public abstract class LexigraphAbstract
    {
        protected string Name { get; set; }

        protected double _from;
        protected double _to;
        protected double _left;
        protected double _right;

        protected double PositiveRef { get; set; }
        protected double NegativeRef { get; set; }

        /// <summary>
        /// Returns reference value of how this double parametr
        /// is referenced to this lexigraphic var
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public double CountReference(double value)
        {
            // laying between top one's vals
            if (_from <= value && value <= _to)
                return 1.0;

            // counting % of reference on LEFT side
            if (_left <= value && value <= _from)
                return (value - _left) / (_from - _left);

            // counting % of reference on RIGHT side
            if (_to <= value && value <= _right)
                return (_right - value) / (_right - _to);

            return 0;
        }
    }
}
