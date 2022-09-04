using System.CommandLine;
using ClingoHeaderParser.Parser;

var fileOption = new Option<FileInfo?>(
    name: "--header",
    description: "Clingo header file (clingo.h) to parse."
)
{ IsRequired = true };

var rootCommand = new RootCommand("A clingo.h file parser to create C# interface");
rootCommand.AddOption(fileOption);
rootCommand.SetHandler((file) => { Parser.ParseHeader(file!); }, fileOption);

return await rootCommand.InvokeAsync(args);