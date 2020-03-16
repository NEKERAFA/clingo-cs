using ClingoSharp.Enums;
using System.Collections.Generic;

namespace ClingoSharp
{
    public class MessageCode : Enumeration
    {
        private static string[] MessageNames => new string[] 
        { 
            "OperationUndefined",
            "RuntimeError",
            "AtomUndefined",
            "FileIncluded",
            "VariableUnbounded",
            "GlobalVariable",
            "Other"
        };

        public static MessageCode OperationUndefined => new MessageCode(0);
        public static MessageCode RuntimeError => new MessageCode(1);
        public static MessageCode AtomUndefined => new MessageCode(2);
        public static MessageCode FileIncluded => new MessageCode(3);
        public static MessageCode VariableUnbounded => new MessageCode(4);
        public static MessageCode GlobalVariable => new MessageCode(5);
        public static MessageCode Other => new MessageCode(6);

        private MessageCode(int value) : base(value, MessageNames[value]) {}

        public new static IEnumerable<string> GetNames()
        {
            return MessageNames;
        }

        public new static IEnumerable<Enumeration> GetValues()
        {
            return new MessageCode[] { OperationUndefined, RuntimeError, AtomUndefined, FileIncluded, VariableUnbounded, GlobalVariable, Other };
        }

        public override int CompareTo(Enumeration other)
        {
            if ((other == null) || !(other is MessageCode))
            {
                return 1;
            }

            return Value.CompareTo(other.Value);
        }

        public override bool Equals(Enumeration other)
        {
            if ((other == null) || !(other is MessageCode))
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        public override string ToString()
        {
            return $"CligoSharp.Warning<{Name}>";
        }
    }
}
