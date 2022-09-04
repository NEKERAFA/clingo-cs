// Based on raylib_parse
// https://github.com/raysan5/raylib/blob/4.2.0/parser/raylib_parser.c
//
// Licensed under zlib/libpng
// Copyright (c) 2021-2022 Ramon Santamaria (@raysan5)
// Copyright (c) 2022 Rafael Alcalde Azpiazu (@nekerafa)

using ClingoHeaderParser.Lexer;

namespace ClingoHeaderParser.Parser
{
    /// <summary>
    /// Define node type
    /// </summary>
    public enum NodeType
    {
        Undefined, Define, Struct, StructField, Enum, EnumValue, Alias, Function, FunctionParam
    }

    /// <summary>
    /// Defines a node
    /// </summary>
    public abstract record Node
    {
        /// <summary>
        /// Node name
        /// </summary>
        public string Name { get; set; } = default!;
        
        /// <summary>
        /// Node description
        /// </summary>
        public string Desc { get; set; } = default!;

        /// <summary>
        /// Node type
        /// </summary>
        public NodeType Type { get; init; } = NodeType.Undefined;
    }

    /// <summary>
    /// Define data
    /// </summary>
    public record DefineInfo : Node
    {
        /// <summary>
        /// Define type
        /// </summary>
        public CType CType { get; set; } = default!;
        /// <summary>
        /// Define value
        /// </summary>
        public Token Value { get; set; } = default!;

        public DefineInfo(string name, string desc, CType type, Token value) : base()
        {
            this.Name = name;
            this.Desc = desc;
            this.CType = type;
            this.Value = value;
            this.Type = NodeType.Define;
        }
    }

    /// <summary>
    /// Struct field info data
    /// </summary>
    public record StructField : Node
    {
        /// <summary>
        /// Struct field type
        /// </summary>
        public CType CType { get; set; } = default!;
        public StructField(string name, string desc, CType type)
        {
            this.Name = name;
            this.Desc = desc;
            this.CType = type;
            this.Type = NodeType.StructField;
        }
    }

    /// <summary>
    /// Struct info data
    /// </summary>
    public record StructInfo : Node
    {
        /// <summary>
        /// Fields in the struct
        /// </summary>
        /// <typeparam name="StructField"></typeparam>
        /// <returns></returns>
        public ICollection<StructField> Fields { get; set; } = new List<StructField>();
        public StructInfo(string name, string desc)
        {
            this.Name = name;
            this.Desc = desc;
            this.Type = NodeType.Struct;
        }
    }

    /// <summary>
    /// Alias info data
    /// </summary>
    public record AliasInfo : Node
    {
        /// <summary>
        /// Alias type
        /// </summary>
        public CType CType { get; set; } = default!;
        public AliasInfo(string name, string desc, CType type)
        {
            this.Name = name;
            this.Desc = desc;
            this.CType = type;
            this.Type = NodeType.Alias;
        }
    }

    /// <summary>
    /// Enum value info data
    /// </summary>
    public record EnumValue : Node
    {
        /// <summary>
        /// Enum value
        /// </summary>
        public uint Value { get; set; } = default!;
        public EnumValue(string name, string desc, uint value)
        {
            this.Name = name;
            this.Desc = desc;
            this.Value = value;
            this.Type = NodeType.EnumValue;
        }
    }

    /// <summary>
    /// Enum info data
    /// </summary>
    public record EnumInfo : Node
    {
        /// <summary>
        /// Values in enumerator
        /// </summary>
        public ICollection<EnumValue> Values { get; set; } = new List<EnumValue>();
        public EnumInfo(string name, string desc)
        {
            this.Name = name;
            this.Desc = desc;
            this.Type = NodeType.Enum;
        }
    }

    /// <summary>
    /// Function param info data
    /// </summary>
    public record FunctionParam : Node
    {
        /// <summary>
        /// Parameter type
        /// </summary>
        public CType CType { get; set; } = default!;
        public FunctionParam(string name, string desc, CType type)
        {
            this.Name = name;
            this.Desc = desc;
            this.CType = type;
            this.Type = NodeType.FunctionParam;
        }
    }

    /// <summary>
    /// Function info data
    /// </summary>
    public record FunctionInfo : Node
    {
        /// <summary>
        /// Return value type
        /// </summary>
        public CType RetType { get; set; } = default!;
        /// <summary>
        /// Function parameters
        /// </summary>
        public ICollection<FunctionParam> Params { get; set; } = new List<FunctionParam>();
        public FunctionInfo(string name, string desc, CType retType)
        {
            this.Name = name;
            this.Desc = desc;
            this.RetType = retType;
            this.Type = NodeType.Enum;
        }
    }

    public class Parser
    {
        public static void ParseHeader(FileInfo file)
        {
            Console.WriteLine("Hello world");
            Console.WriteLine($"file: {file.FullName}");
        }
    }
}