using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyTreeLib.Models.Ref
{
    public class ReferenceState
    {
        public double PosRef { get; set; }
        public double NegRef { get; set; }
        public double SharedRef { get; set; }

        public ReferenceState() { }

        public ReferenceState(double pos, double neg, double shared)
        {
            PosRef = pos;
            NegRef = neg;
            SharedRef = shared;
        }

        public override string ToString()
        {
            return $"Ref: pos={PosRef}; neg={NegRef}; ref={SharedRef}";
        }
    }
}
