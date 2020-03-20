using ClingoSharp;
using System;
using System.Collections.Generic;

namespace ClingoSharpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Clingo {Clingo.Version}\n");

            Control ctl = new Control(new List<string>() { "0" });
            ctl.Add("base", new List<string>(), "{a; b}.");
            ctl.Ground(new List<Tuple<string, List<Symbol>>>() { new Tuple<string, List<Symbol>>("base", new List<Symbol>()) });
            
            var handle = (SolveHandle)ctl.Solve(yield: true);
            var models = new List<Symbol>();

            foreach (var model in handle)
            {
                var symbols = model.GetSymbols(atoms: true);
                models.AddRange(symbols);
            }
        }
    }
}
