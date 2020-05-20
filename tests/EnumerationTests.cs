using ClingoSharp.Enums;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClingoSharp.Tests
{
    [TestFixture]
    class EnumerationTests
    {
        [Test]
        public void MessageCode_GetNames_Test()
        {
            IEnumerable<string> names = Enumeration.GetNames<MessageCode>();
            string[] expectedValues =
            {
                "OperationUndefined",
                "RuntimeError",
                "AtomUndefined",
                "FileIncluded",
                "VariableUnbounded",
                "GlobalVariable",
                "Other"
            };

            CollectionAssert.AreEquivalent(expectedValues, names);
        }

        [Test]
        public void MessageCode_Values_Test()
        {
            IEnumerable<MessageCode> values = Enumeration.GetValues<MessageCode>();
            MessageCode[] expectedValues =
            {
                MessageCode.OperationUndefined,
                MessageCode.RuntimeError,
                MessageCode.AtomUndefined,
                MessageCode.FileIncluded,
                MessageCode.VariableUnbounded,
                MessageCode.GlobalVariable,
                MessageCode.Other
            };

            CollectionAssert.AreEquivalent(expectedValues, values);
        }

        [Test]
        public void MessageCode_Value_Tests()
        {
            MessageCode messageCode = Enumeration.GetValue<MessageCode>(0);
            Assert.AreEqual(MessageCode.OperationUndefined, messageCode);

            messageCode = Enumeration.GetValue<MessageCode>(1);
            Assert.AreEqual(MessageCode.RuntimeError, messageCode);

            messageCode = Enumeration.GetValue<MessageCode>(2);
            Assert.AreEqual(MessageCode.AtomUndefined, messageCode);

            messageCode = Enumeration.GetValue<MessageCode>(3);
            Assert.AreEqual(MessageCode.FileIncluded, messageCode);

            messageCode = Enumeration.GetValue<MessageCode>(4);
            Assert.AreEqual(MessageCode.VariableUnbounded, messageCode);

            messageCode = Enumeration.GetValue<MessageCode>(5);
            Assert.AreEqual(MessageCode.GlobalVariable, messageCode);

            messageCode = Enumeration.GetValue<MessageCode>(6);
            Assert.AreEqual(MessageCode.Other, messageCode);

            Assert.Throws<ArgumentOutOfRangeException>(() => { Enumeration.GetValue<MessageCode>(-1); });
            Assert.Throws<ArgumentException>(() => { Enumeration.GetValue(typeof(object), 0); });
        }

        [Test]
        public void MessageCode_Parse_Test()
        {
            MessageCode messageCode = Enumeration.Parse<MessageCode>("OperationUndefined", false);
            Assert.AreEqual(MessageCode.OperationUndefined, messageCode);

            messageCode = Enumeration.Parse<MessageCode>("runtimeError", true);
            Assert.AreEqual(MessageCode.RuntimeError, messageCode);

            messageCode = Enumeration.Parse<MessageCode>("AtomUndefined");
            Assert.AreEqual(MessageCode.AtomUndefined, messageCode);

            messageCode = Enumeration.Parse<MessageCode>("FileIncluded", false);
            Assert.AreEqual(MessageCode.FileIncluded, messageCode);

            messageCode = Enumeration.Parse<MessageCode>("variableUnbounded", true);
            Assert.AreEqual(MessageCode.VariableUnbounded, messageCode);

            messageCode = Enumeration.Parse<MessageCode>("GlobalVariable");
            Assert.AreEqual(MessageCode.GlobalVariable, messageCode);

            messageCode = Enumeration.Parse<MessageCode>("Other");
            Assert.AreEqual(MessageCode.Other, messageCode);

            Assert.Throws<ArgumentNullException>(() => { Enumeration.Parse<MessageCode>(null); });
            Assert.Throws<ArgumentException>(() => { Enumeration.Parse(typeof(MessageCode), " "); });
            Assert.Throws<ArgumentException>(() => { Enumeration.Parse<MessageCode>("other"); });
            Assert.Throws<ArgumentException>(() => { Enumeration.Parse(typeof(object), "Error"); });
        }

        [Test]
        public void ModelType_GetNames_Test()
        {
            IEnumerable<string> names = Enumeration.GetNames(typeof(ModelType));
            string[] expectedValues =
            {
                "StableModel",
                "BraveConsequences",
                "CautiousConsequences"
            };

            CollectionAssert.AreEquivalent(expectedValues, names);
        }

        [Test]
        public void ModelType_Values_Test()
        {
            IEnumerable<ModelType> values = Enumeration.GetValues(typeof(ModelType)).Cast<ModelType>();
            ModelType[] expectedValues =
            {
                ModelType.StableModel,
                ModelType.BraveConsequences,
                ModelType.CautiousConsequences
            };

            CollectionAssert.AreEquivalent(expectedValues, values);
        }

        [Test]
        public void ModelType_Value_Tests()
        {
            ModelType modelType = (ModelType)Enumeration.GetValue(typeof(ModelType), 0);
            Assert.AreEqual(ModelType.StableModel, modelType);

            modelType = (ModelType)Enumeration.GetValue(typeof(ModelType), 1);
            Assert.AreEqual(ModelType.BraveConsequences, modelType);

            modelType = (ModelType)Enumeration.GetValue(typeof(ModelType), 2);
            Assert.AreEqual(ModelType.CautiousConsequences, modelType);

            Assert.Throws<ArgumentOutOfRangeException>(() => { Enumeration.GetValue<ModelType>(3); });
        }

        [Test]
        public void ModelType_Parse_Test()
        {
            ModelType modelType = (ModelType)Enumeration.Parse(typeof(ModelType), "StableModel", false);
            Assert.AreEqual(ModelType.StableModel, modelType);

            modelType = (ModelType)Enumeration.Parse(typeof(ModelType), "braveConsequenceS", true);
            Assert.AreEqual(ModelType.BraveConsequences, modelType);

            modelType = (ModelType)Enumeration.Parse(typeof(ModelType), "CautiousConsequences");
            Assert.AreEqual(ModelType.CautiousConsequences, modelType);

            Assert.Throws<ArgumentNullException>(() => { Enumeration.Parse(typeof(ModelType), null); });
            Assert.Throws<ArgumentException>(() => { Enumeration.Parse(typeof(ModelType), " "); });
            Assert.Throws<ArgumentException>(() => { Enumeration.Parse(typeof(ModelType), "stableModel"); });
        }

        [Test]
        public void SymbolType_GetNames_Test()
        {
            IEnumerable<string> names = Enumeration.GetNames(typeof(SymbolType));
            string[] expectedValues =
            {
                "Infimum",
                "Number",
                "String",
                "Function",
                "Supremum"
            };

            CollectionAssert.AreEquivalent(expectedValues, names);
        }

        [Test]
        public void SymbolType_Values_Test()
        {
            IEnumerable<SymbolType> values = Enumeration.GetValues(typeof(SymbolType)).Cast<SymbolType>();
            SymbolType[] expectedValues =
            {
                SymbolType.Infimum,
                SymbolType.Number,
                SymbolType.String,
                SymbolType.Function,
                SymbolType.Supremum
            };

            CollectionAssert.AreEquivalent(expectedValues, values);
        }

        [Test]
        public void SymbolType_Value_Tests()
        {
            SymbolType symbolType = (SymbolType)Enumeration.GetValue(typeof(SymbolType), 0);
            Assert.AreEqual(SymbolType.Infimum, symbolType);

            symbolType = (SymbolType)Enumeration.GetValue(typeof(SymbolType), 1);
            Assert.AreEqual(SymbolType.Number, symbolType);

            symbolType = (SymbolType)Enumeration.GetValue(typeof(SymbolType), 2);
            Assert.AreEqual(SymbolType.String, symbolType);

            symbolType = (SymbolType)Enumeration.GetValue(typeof(SymbolType), 3);
            Assert.AreEqual(SymbolType.Function, symbolType);

            symbolType = (SymbolType)Enumeration.GetValue(typeof(SymbolType), 4);
            Assert.AreEqual(SymbolType.Supremum, symbolType);

            Assert.Throws<ArgumentOutOfRangeException>(() => { Enumeration.GetValue<SymbolType>(5); });
        }

        [Test]
        public void SymbolType_Parse_Test()
        {
            SymbolType symbolType = (SymbolType)Enumeration.Parse(typeof(SymbolType), "Infimum", false);
            Assert.AreEqual(SymbolType.Infimum, symbolType);

            symbolType = (SymbolType)Enumeration.Parse(typeof(SymbolType), "number", true);
            Assert.AreEqual(SymbolType.Number, symbolType);

            symbolType = (SymbolType)Enumeration.Parse(typeof(SymbolType), "String");
            Assert.AreEqual(SymbolType.String, symbolType);

            symbolType = Enumeration.Parse<SymbolType>("Function");
            Assert.AreEqual(SymbolType.Function, symbolType);

            symbolType = Enumeration.Parse<SymbolType>("supremum", true);
            Assert.AreEqual(SymbolType.Supremum, symbolType);

            Assert.Throws<ArgumentNullException>(() => { Enumeration.Parse(typeof(SymbolType), null); });
            Assert.Throws<ArgumentException>(() => { Enumeration.Parse(typeof(SymbolType), " "); });
            Assert.Throws<ArgumentException>(() => { Enumeration.Parse(typeof(SymbolType), "infimun"); });
        }
    }
}
