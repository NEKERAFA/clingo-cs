using ClingoSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ClingoSharpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Clingo {Clingo.Version}\n");

            Control ctl = new Control(new List<string>() { "0" });

            string filename = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "program.lp");
            Console.WriteLine(filename);
            ctl.Load(filename);

            
            //ctl.Add("base", new List<string>() { }, "{a; b}.");
            var parts = new List<Tuple<string, List<Symbol>>>()
            {
                new Tuple<string, List<Symbol>>("base", new List<Symbol>() {}),
            };

            ctl.Ground(parts);

            // Using lockable call
            //ctl.Solve(onModel: m => { Console.WriteLine($"Answer: {m}"); return true; });

            // Using yieldable call
            //SolveHandle handle = ctl.Solve(yield: true);
            //foreach (var m in handle)
            //{
            //    Console.WriteLine($"Answer: {m}");
            //    handle.Get();
            //}

            // Using async call
            SolveHandle handle = ctl.Solve(onModel: m => { Console.WriteLine($"Answer: {m}"); return true; }, async: true);
            while (!handle.Wait(0))
            {
                continue;
            }
            
        }
    }
}