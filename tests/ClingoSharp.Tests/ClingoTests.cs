using ClingoSharp;
using NUnit.Framework;

namespace ClingoSharp.Tests
{
    public class ClingoTests
    {
        [Test]
        public void GetVersion()
        {
            string result = Clingo.Version;
            Assert.AreEqual("5.4.0", result);
        }
    }
}