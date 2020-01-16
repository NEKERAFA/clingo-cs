using ClingoSharp;
using System;
using System.Collections.Generic;

namespace ClingoSharpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world from clingo " + Clingo.Version);
            Console.WriteLine();

            Control control = new Control();
            control.Add("base", null, "p.");
            control.Ground(new List<Tuple<string, List<Symbol>>>());
            control.Solve();

            Console.ReadKey();
        }
    }
}
