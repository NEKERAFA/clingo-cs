using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ClingoSharp.Symbols;
using FluentAssertions;
using Xunit;

namespace ClingoSharp.Tests
{
    public class SymbolTests
    {
        [Fact]
        public void CheckSymbol()
        {
            List<Symbol> args = new();

            var sym = new Symbol(42);
            sym.Type.Should().Be(SymbolType.Number);
            sym.Number.Should().Be(42);
            args.Add(sym);

            sym = Symbol.Infimum;
            sym.Type.Should().Be(SymbolType.Infimum);
            args.Add(sym);

            sym = Symbol.Supremum;
            sym.Type.Should().Be(SymbolType.Supremum);
            args.Add(sym);

            sym = new Symbol("x");
            sym.Type.Should().Be(SymbolType.String);
            sym.String.Should().Be("x");
            args.Add(sym);

            sym = new Symbol("x", false);
            sym.Type.Should().Be(SymbolType.Function);
            sym.IsNegative.Should().BeTrue();
            sym.IsPositive.Should().BeFalse();
            sym.Name.Should().Be("x");
            args.Add(sym);

            sym = new Symbol("f", args);
            sym.Type.Should().Be(SymbolType.Function);
            sym.IsNegative.Should().BeFalse();
            sym.IsPositive.Should().BeTrue();
            sym.Name.Should().Be("f");
            sym.ToString().Should().Be("f(42,#inf,#sup,\"x\",-x)");
            sym.Arguments.Should().HaveCount(5).And.Equal(args);

            #pragma warning disable CS1718

            var a = (Symbol)1; var b = (Symbol)2;
            Assert.True(a < b);
            Assert.False(a < a);
            Assert.False(b < a);
            Assert.True(b > a);
            Assert.False(a > a);
            Assert.False(a > b);
            Assert.True(a <= a);
            Assert.True(a <= b);
            Assert.False(b <= a);
            Assert.True(a >= a);
            Assert.True(b >= a);
            Assert.False(a >= b);
            Assert.True(a == a);
            Assert.False(a == b);
            Assert.True(a != b);
            Assert.False(a != a);
            Assert.Equal(0, a.CompareTo(a));
            Assert.Equal(-1, a.CompareTo(b));
            Assert.Equal(1, b.CompareTo(a));
            Assert.Equal(a.GetHashCode(), a.GetHashCode());
            Assert.NotEqual(a.GetHashCode(), b.GetHashCode());

            #pragma warning restore CS1718
        }
    }
}