using Xunit;

namespace Clingo_cs.Tests
{
    public class ClingoTests
    {
        [Fact]
        public void GetVersion()
        {
            (int, int, int) result = Clingo.Version();
            Assert.Equal(5, result.Item1);
            Assert.Equal(5, result.Item2);
            Assert.Equal(1, result.Item3);
        }
    }
}
