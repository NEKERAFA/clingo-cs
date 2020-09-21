using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClingoSharp.Tests
{
    [TestFixture]
    public class ControlTests
    {
        private static Clingo clingo = null;

        [SetUp]
        public static void LoadClingo()
        {
            clingo = new Clingo();
        }

        [Test]
        public void LoadFile()
        {
            using Control control = new Control();
            string filename = Path.Combine(TestContext.CurrentContext.TestDirectory, "files", "model_a.lp");
            control.Load(filename);

            var parts = new List<Tuple<string, List<Symbol>>>()
            {
                new Tuple<string, List<Symbol>>("base", new List<Symbol>() {}),
            };

            control.Ground(parts);

            List<string> models = new List<string>();
            SolveHandle handle = control.Solve(onModel: m =>
            {
                models.Add(m.ToString());
                return true;
            }, async: true);

            while (!handle.Wait(0)) handle.Get();

            Assert.AreEqual(1, models.Count);
            Assert.AreEqual("a", models[0]);
        }

        [Test]
        public void TestSymbol()
        {
            using Control control = new Control();
            control.Add("base", null, "f(1, a). f(2, a). f(3, b).");

            var parts = new List<Tuple<string, List<Symbol>>>()
            {
                new Tuple<string, List<Symbol>>("base", new List<Symbol>() {}),
            };

            control.Ground(parts);

            int count = 0;
            SolveHandle handle = control.Solve(onModel: model =>
            {
                foreach (Symbol symbol in model.GetSymbols(shown: true))
                {
                    Assert.AreEqual("f", symbol.Name);
                    Assert.AreEqual(2, symbol.Arguments.Count);
                    Assert.That(symbol.Arguments[0].Number, Is.InRange(1, 3));
                    Assert.That(symbol.Arguments[1].Name, Is.EqualTo("a").Or.EqualTo("b"));
                    count++;
                }

                return true;
            }, async: true);

            while (!handle.Wait(0)) handle.Get();

            Assert.AreEqual(3, count);
        }
    }
}