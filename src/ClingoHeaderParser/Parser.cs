// Under MIT License
//
// Copyright (c) 2020 Rafael Alcalde Azpiazu (nekerafa)

using CppAst;

namespace ClingoHeaderParser.Parser
{
    public class Parser
    {
        private static CppParserOptions? options = null;

        static Parser()
        {
            options = new()
            {
                ParseAsCpp = true,
                TargetSystem = "linux"
            };
        }

        public static void ParseHeader(FileInfo header, FileInfo output)
        {
            Console.WriteLine($"File: {header.FullName}");
            var compilation = CppParser.ParseFile(header.FullName, options);

            Console.WriteLine($"Errors: {compilation.HasErrors}");
            if (compilation.HasErrors)
            {
                foreach (var message in compilation.Diagnostics.Messages) {
                    Console.WriteLine(message);
                }

                return;
            }
        }
    }
}