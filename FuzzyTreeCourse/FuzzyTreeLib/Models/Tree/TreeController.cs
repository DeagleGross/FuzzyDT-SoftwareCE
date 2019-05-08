using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.Loaders;
using FuzzyTreeLib.Models.Ref;
using Shields.GraphViz.Models;

namespace FuzzyTreeLib.Models.Tree
{
    public class TreeController
    {
        public bool IsConstructed { get; set; }
        public AtributNode Root { get; set; }
        private List<ReferenceState> References { get; set; }


        /// <summary>
        /// default costructor
        /// </summary>
        public TreeController()
        {
            IsConstructed = false;
        }

        /// <summary>
        /// Returns collection of EdgeStatement from whole Tree
        /// </summary>
        /// <returns></returns>
        public HashSet<KeyValuePair<string,string>> GetTreeEdgeStatements()
        {
            HashSet<KeyValuePair<string, string>> edges = new HashSet<KeyValuePair<string, string>>();

            // going in recursion from root
            Root.GetEdgeStatements(edges);

            return edges;
        }


        /// <summary>
        /// Constructs a tree if it not constructed at all
        /// </summary>
        public void ConstructTree(List<RefAtribut> refAtributs, bool toSort)
        {
            if (this.IsConstructed == true)
            {
                WriteDownTree();
                return;
            }

            // sorted due to InfoGrowthFunction
            List<RefAtribut> refSorted = SortAndDefaultRefAtributs(refAtributs);

            // watching what happened
            Console.WriteLine(ToStringRefs(refSorted));

            // created root. It has no parent!
            Root = new AtributNode(refSorted[0].Name, null);
            Root.AddChildrenForNode(refSorted[0].Atribut);

            // Going from all NOT ROOT atributs
            for (int i = 1; i < refSorted.Count; ++i)
            {
                AddAtributNodeToTree(refSorted[i]);
            }

            WriteDownTree();
            this.IsConstructed = true;
        }

        private void AddAtributNodeToTree(RefAtribut refAtribut)
        {
            Root.AddChildAtributNode(refAtribut);
        }

        /// <summary>
        /// Sorts passed list by growth value function value.
        /// Sorting FROM HIGH TO LOW.
        /// Sets property "IsLeaf" for last RefAtribut to true
        /// Defaults all isLeaf and isUsed = false
        /// </summary>
        /// <param name="refAtributs"></param>
        private List<RefAtribut> SortAndDefaultRefAtributs(List<RefAtribut> refAtributs)
        {
            refAtributs.Sort((x, y) => y.InfoGrowth.CompareTo(x.InfoGrowth));

            return refAtributs;
        }

        private double resultFunction(List<ReferenceState> refs)
        {
            double upper = 0;
            double lower = 0;

            foreach (ReferenceState refVar in refs)
            {
                // for pos and negative References
                // for negative it can be ignored - multiply on zero
                    upper += 1.0 * refVar.SharedRef * refVar.PosRef;
                    //upper += 0.0 * refVar.SharedRef * refVar.NegRef;

                // for lower not ignoring anything
                    lower += refVar.SharedRef * (refVar.NegRef + refVar.PosRef);
            }

            return (lower == 0) ? upper : upper / lower;
        }

        /// <summary>
        /// returns result from all inputs
        /// FINAL AND MAIN FUNCTION - COUNTING RESULT
        /// </summary>
        public double GetTheResult(List<InputValue> inputValues)
        {
            if (!IsConstructed)
            {
                Console.WriteLine("Tree is not Constructed!\nUse API functions to contsruct it first");
                return 0;
            }

            // refreshing lists for counting
            References = new List<ReferenceState>();

            Root.PushInputThroughTree(inputValues, References);

            // logging to console
            WriteDownReferences();

            return resultFunction(References);
        }

        public string ToStringRefs(List<RefAtribut> refAtributs)
        {
            string str = "";

            str += "\n\nReporting info about RefAtributs list:::\n";

            foreach (var refAtribut in refAtributs)
                str += refAtribut.ToString();

            return str;
        }

        public void WriteDownReferences()
        {
            foreach (var reference in References)
            {
                Console.WriteLine(reference);
            }
        }

        public void WriteDownTree()
        {
            Console.WriteLine("\n\n" + this + "\n\n");
        }

        public override string ToString()
        {
            return $"\n\n------------TREE :: \n{Root.ToString("  ")}";
        }
    }
}
