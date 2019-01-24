using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.AbstractInterfaces;
using FuzzyTreeLib.Models.Loaders;
using FuzzyTreeLib.Models.Ref;

namespace FuzzyTreeLib.Models.Tree
{
    public class LexigraphNode : LexigraphAbstract
    {
        public double Ref { get; set; }
        public AtributNode ParentAtribut { get; set; }
        public AtributNode ChildAtribut { get; set; }

        public double MinPosRef { get; set; }
        public double MinNegRef { get; set; }
        public double MinSharedRef { get; set; }

        public string GetName => Name;

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
        public LexigraphNode(string name, double left, double from, double to, double right)
        {
            this.Name = name;

            this._from = from;
            this._to = to;

            this._left = left;
            this._right = right;
        }

        public LexigraphNode(AtributNode parent, RefLexigraphic refLexigraphic)
        {
            double[] funcPoints = refLexigraphic.GetReferenceFunctionPoints();

            this.Name = refLexigraphic.GetName;

            this._left = funcPoints[0];
            this._from = funcPoints[1];
            this._to = funcPoints[2];
            this._right = funcPoints[3];

            this.NegativeRef = refLexigraphic.GetNegativeRefValue;
            this.PositiveRef = refLexigraphic.GetPositiveRefValue;

            this.ParentAtribut = parent;
        }

        /// <summary>
        /// Counts Ref and searches min ref params in upperSubTree
        /// </summary>
        /// <param name="value"></param>
        /// <param name="inputValues"></param>
        /// <param name="references"></param>
        public void PushInputThroughTree(double value, List<InputValue> inputValues, List<ReferenceState> references)
        {
            Ref = CountReference(value);

            LexigraphNode parentLexigraphNode = this.ParentAtribut.ParentLexigraph;

            // setting this as min ones
            if (parentLexigraphNode == null)
            {
                if (!Ref.Equals(0))
                {
                    MinNegRef = NegativeRef;
                    MinPosRef = PositiveRef;
                    MinSharedRef = Ref;
                }
            }
            else // counting min using also parentLexigraph
            {
                MinNegRef = (NegativeRef < parentLexigraphNode.MinNegRef) 
                    ? NegativeRef 
                    : parentLexigraphNode.MinNegRef;

                MinPosRef = (PositiveRef < parentLexigraphNode.MinPosRef)
                    ? PositiveRef
                    : parentLexigraphNode.MinPosRef;

                MinSharedRef = (Ref < parentLexigraphNode.MinSharedRef)
                    ? Ref
                    : parentLexigraphNode.MinSharedRef;
            }

            // launching Child Atributs function
            // or loading results to final list with coefficients
            if (this.ChildAtribut == null )
            {               
                // not doing it to reference = 0
                // it is not needed to add zero refs
                if (!MinSharedRef.Equals(0.0))
                    references.Add(new ReferenceState(MinPosRef, MinNegRef, MinSharedRef));
            }
            else
            {
                this.ChildAtribut.PushInputThroughTree(inputValues, references);
            }
        }

        public void AddChildAtributNode(RefAtribut refAtribut)
        {
            if (ChildAtribut != null)
            {
                this.ChildAtribut.AddChildAtributNode(refAtribut);
            }
            else
            {
                // HERE is the code of addition new AtributNode
                ChildAtribut = new AtributNode(refAtribut.Name, this);
                ChildAtribut.AddChildrenForNode(refAtribut.Atribut);
            }
        }

        public string ToString(string tab)
        {
            string str = "";

            if (ChildAtribut != null)
                str += tab + $"{Name}::Parent={ParentAtribut.Name}::Child={ChildAtribut}, Neg={NegativeRef}, Pos={PositiveRef}, Ref={Ref}\n";
            else
                str += tab + $"{Name}::Parent={ParentAtribut.Name}::Child=null, Neg={NegativeRef}, Pos={PositiveRef}, Ref={Ref}\n";

            if (this.ChildAtribut != null)
                str += ChildAtribut.ToString(tab + tab);

            return str;
        }
    }
}
