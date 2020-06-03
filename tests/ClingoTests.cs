using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace ClingoSharp.Tests
{
    public class ClingoTests
    {
        [Test]
        public void GetVersion()
        {
            string currentPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).AbsolutePath);
            Clingo clingo = new Clingo(Path.Combine(currentPath));
            string result = clingo.Version;
            Assert.AreEqual("5.4.0", result);
        }
    }
}