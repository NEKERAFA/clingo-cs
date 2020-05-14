using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClingoSharp.Tests
{
    [TestFixture]
    public class ControlTests
    {
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
            control.Solve(onModel: m =>
            {
                models.Add(m.ToString());
                return true;
            });

            Assert.AreEqual(1, models.Count);
            Assert.AreEqual("a", models[0]);
        }
    }
}