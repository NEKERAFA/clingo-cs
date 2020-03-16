using System;
using System.Collections.Generic;
using System.Linq;

namespace ClingoSharp.Enums
{
    public abstract class Enumeration : IEnumeration, IComparable<Enumeration>, IEquatable<Enumeration>
    {
        public int Value { get; private set; }
        public string Name { get; private set; }

        protected Enumeration(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public static IEnumerable<string> GetNames()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Enumeration> GetValues()
        {
            throw new NotImplementedException();
        }

        public static Enumeration GetName(int value)
        {
            return GetValues().FirstOrDefault(enumeration => enumeration.Value == value);
        }

        public static Enumeration Parse(string name, bool ignoreCase)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (name == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            if (!TryParse(name, ignoreCase, out Enumeration enumeration))
            {
                throw new ArgumentException();
            }

            return enumeration;
        }

        public static bool TryParse(string name, out Enumeration enumeration)
        {
            return TryParse(name, false, out enumeration);
        }

        public static bool TryParse(string name, bool ignoreCase, out Enumeration enumeration)
        {
            enumeration = GetValues().FirstOrDefault(symbolType => symbolType.Name.Equals(name, ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture));
            return enumeration == default;
        }

        public abstract bool Equals(Enumeration other);

        public abstract int CompareTo(Enumeration other);

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration))
            {
                return false;
            }

            var other = obj as Enumeration;
            return Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode() ^ Name.GetHashCode();
        }
    }
}
