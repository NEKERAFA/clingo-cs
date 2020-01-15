using ClingoSharp;
using System;

namespace ClingoSharpApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world from clingo " + Clingo.Version);

            Control control = new Control();

            Console.ReadKey();
        }
    }
}
