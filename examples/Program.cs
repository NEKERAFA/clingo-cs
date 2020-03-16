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

            Control ctl = new Control();
            ctl.Add("base", new List<string>(), "a.");
            ctl.Ground(new List<Tuple<string, List<Symbol>>>() { new Tuple<string, List<Symbol>>("base", new List<Symbol>()) });
            var solveResult = (SolveResult)ctl.Solve();

            Console.WriteLine($"Satifiable: {solveResult.IsSatisfiable}");
            Console.WriteLine($"Unsatisfiable: {solveResult.IsUnSatisfiable}");
            Console.WriteLine($"Unknown: {solveResult.IsUnknown}");
            Console.WriteLine($"Exhausted: {solveResult.IsExhausted}");
            Console.WriteLine($"Interrupted: {solveResult.IsInterrupted}");
        }
    }
}
