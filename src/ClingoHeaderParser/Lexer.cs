using System.Text;

namespace ClingoHeaderParser.Lexer
{
    /// <summary>
    /// Type of the tokens
    /// </summary>
    public enum TokenType
    {
        Undefined, Space, Literal, Number, Symbol, Comment, Define, TypeDef, Struct, Enum, CType
    }

    /// <summary>
    /// Represents a token
    /// </summary>
    public abstract record Token
    {
        public TokenType Type { get; init; } = TokenType.Undefined;
    }

    /// <summary>
    /// Represents a space
    /// </summary>
    public sealed record Space : Token
    {
        public Space()
        {
            this.Type = TokenType.Space;
        }
    }

    /// <summary>
    /// Represents a string
    /// </summary>
    public sealed record Literal : Token
    {
        public StringBuilder Value { get; set; } = new();
        public Literal()
        {
            this.Type = TokenType.Literal;
        }
    }

    /// <summary>
    /// Represents a number
    /// </summary>
    public sealed record Number : Token
    {
        public StringBuilder Value { get; set; } = new();
        public bool IsDecimal { get; set; } = false;
        public Number()
        {
            this.Type = TokenType.Number;
        }
    }

    /// <summary>
    /// Represents a symbol
    /// </summary>
    public sealed record Symbol : Token
    {
        public char Value { get; set; } = '\0';
        public Symbol()
        {
            this.Type = TokenType.Symbol;
        }
    }

    /// <summary>
    /// Represents a start comment line
    /// </summary>
    public sealed record Comment : Token
    {
        public Comment()
        {
            this.Type = TokenType.Comment;
        }
    }

    /// <summary>
    /// Represents a <c>define</c> preprocessor directive
    /// </summary>
    public sealed record Define : Token
    {
        public Define()
        {
            this.Type = TokenType.Define;
        }
    }

    /// <summary>
    /// Represents a <c>typedef</c> keyword
    /// </summary>
    public sealed record TypeDef : Token
    {
        public TypeDef()
        {
            this.Type = TokenType.TypeDef;
        }
    }

    /// <summary>
    /// Represents a <c>struct</c> keyword
    /// </summary>
    public sealed record Struct : Token
    {
        public Struct()
        {
            this.Type = TokenType.Struct;
        }
    }

    /// <summary>
    /// Represents an <c>enum</c> keyword
    /// </summary>
    public sealed record Enum : Token
    {
        public Enum()
        {
            this.Type = TokenType.Enum;
        }
    }

    /// <summary>
    /// Represents a C type
    /// </summary>
    public sealed record CType : Token
    {
        public StringBuilder Value { get; set; } = new();
        public StringBuilder Modifier { get; set; } = new();
        public bool IsPointer { get; set; } = false;
        public CType()
        {
            this.Type = TokenType.CType;
        }
    }
}