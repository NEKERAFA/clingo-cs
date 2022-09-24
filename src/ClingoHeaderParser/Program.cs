// Under MIT License
//
// Copyright (c) 2020 Rafael Alcalde Azpiazu (nekerafa)

using System.CommandLine;
using ClingoHeaderParser.Parser;

var headerOption = new Option<FileInfo?>(
    name: "--header",
    description: "Clingo header file (clingo.h) to parse.",
    parseArgument: result =>
    {
        var path = result.Tokens.Single().Value;
        var file = new FileInfo(path);

        if (!file.Exists)
        {
            result.ErrorMessage = $"'{file.FullName}' file not found";
            return null;
        }

        return file;
    }
)
{ IsRequired = true };
headerOption.AddAlias("-h");

var outputOption = new Option<FileInfo>(
    name: "--output",
    description: "Clingo C# interface file output",
    getDefaultValue: () => new("Clingo_c.cs")
);
outputOption.AddAlias("-o");

var forceOutput = new Option<bool>(
    name: "--force",
    description: "If output file exists, override their content",
    getDefaultValue: () => false
);
forceOutput.AddAlias("-f");

var rootCommand = new RootCommand("A clingo.h file parser to create C# interface");
rootCommand.AddOption(headerOption);
rootCommand.AddOption(outputOption);
rootCommand.AddOption(forceOutput);
rootCommand.SetHandler(
    (header, output, force) =>
    {
        if (output.Exists && !force)
        {
            Console.WriteLine($"'{output.FullName}' exists. Do you want to override it?");
        }

        Parser.ParseHeader(header!, output);
    }, 
    headerOption, outputOption, forceOutput
);

return await rootCommand.InvokeAsync(args);