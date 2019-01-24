using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FuzzyTreeLib.Models.Loaders;
using FuzzyTreeLib.Models.Ref;

namespace FuzzyTreeLib.Models.Tree
{
    public class AtributNode
    {
        public string Name { get; set; }
        public List<LexigraphNode> ChildrenLexigraphs { get; set; }
        public LexigraphNode ParentLexigraph { get; set; }

        public AtributNode(string name, LexigraphNode parent)
        {
            this.Name = name;
            this.ParentLexigraph = parent;
            this.ChildrenLexigraphs = new List<LexigraphNode>();
        }

        public void AddChildAtributNode(RefAtribut refAtribut)
        {
            foreach (var childrenLexigraph in ChildrenLexigraphs)
            {
                childrenLexigraph.AddChildAtributNode(refAtribut);
            }
        }

        /// <summary>
        /// sets children to this atributNode
        /// </summary>
        /// <param name="lexigraphics"></param>
        public void AddChildrenForNode(List<RefLexigraphic> lexigraphics)
        {
            foreach (var lexigraph in lexigraphics)
            {
                ChildrenLexigraphs.Add
                (
                    new LexigraphNode(this, lexigraph)
                );
            }
        }

        /// <summary>
        /// Pushes input and fills Lists of 
        /// </summary>
        /// <param name="inputValues"></param>
        /// <param name="Pos"></param>
        /// <param name="Neg"></param>
        /// <param name="Ref"></param>
        public void PushInputThroughTree(List<InputValue> inputValues, List<ReferenceState> references)
        {
            // to avoid errors
            if (ChildrenLexigraphs.Count == 0)
                return;

            // getting value
            double inputValForThisAtribut = inputValues.Find(
                item => item.Name.Equals(this.Name)).Value;

            foreach (var lexigraphNode in this.ChildrenLexigraphs)
            {
                lexigraphNode.PushInputThroughTree(inputValForThisAtribut, inputValues, references);
            }
        }

        public string ToString(string tab)
        {
            string str = "";

            if (ParentLexigraph == null)
                str += tab + $"{this.Name}::Parent=null\n";
            else
                str += tab + $"{this.Name}::Parent={this.ParentLexigraph.GetName}\n";
            
            
            str += tab + "  Children::";

            if (ChildrenLexigraphs.Count == 0)
                str += "NULL\n";
            else
            {
                str += "\n";
                foreach (LexigraphNode lexNode in this.ChildrenLexigraphs)
                    str += tab + lexNode.ToString(tab) + "\n";
            }
                

            return str;
        }
    }
}
