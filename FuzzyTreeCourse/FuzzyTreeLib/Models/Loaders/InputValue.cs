using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyTreeLib.Models.Loaders
{
    public class InputValue
    {
        public string Name { get; set; }
        public double Value { get; set; }

        public InputValue() { }

        public InputValue(string Name)
        {
            this.Name = Name;

        }

        private double ReadDouble()
        {
            double ret;
            Console.Write($"{Name}::Your Input = ");

            while (!double.TryParse(Console.ReadLine(), out ret))
            {
                Console.WriteLine($"Input is wrong. Repeat.");
                Console.Write($"{ Name}::Your Input = ");
            }

            return ret;
        }

        /// <summary>
        /// fills the Value field 
        /// </summary>
        public void AskAndFill()
        {
            this.Value = ReadDouble();
        }
    }
}
