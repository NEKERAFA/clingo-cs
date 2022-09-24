# clingo-cs

[![.NET 6.0](https://img.shields.io/badge/.NET-6.0-7014e8)](https://dotnet.microsoft.com/download/dotnet/6.0)
[![Clingo 5.5.2](https://img.shields.io/badge/Clingo-5.5.2-blue)](https://github.com/potassco/clingo/tree/v5.5.2)
![ClingoSharp Workflow](https://github.com/NEKERAFA/ClingoSharp/workflows/ClingoSharp%20Workflow/badge.svg?branch=master&event=push)
[![Coverage Status](https://coveralls.io/repos/github/NEKERAFA/ClingoSharp/badge.svg?branch=master)](https://coveralls.io/github/NEKERAFA/ClingoSharp?branch=master)

C# bindings of [clingo](https://github.com/potassco/clingo) 5.5.1, an ASP system to ground and solve logic programs.

## Requirements

* Windows 10 or Ubuntu 22.04
* [.NET 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
* Visual Studio 19 or GCC, and CMake

## Development

`Clingo_cs` uses the clingo Python API as reference. Instead, `Clingo_c` refers to the clingo C API. You can see all the clingo-cs API reference in https://nekerafa.github.io/clingo_cs.

### Packages

#### Clingo_cs

*Working in progress...*

#### Clingo_c

*Working in progress...*

## Build

Clone the repository and the submodules:

```
$ git clone --recurse-submodules -j8 git://github.com/nekerafa/clingo-cs.git
```

Execute the build script (*.ps1* in Windows or *.sh* in GNU/Linux) to build clingo:

```
$ .\build.ps1
```

Compile the .NET project

```
$ dotnet build --configuration Debug
```

## Test

With the project built, you can execute the *test.ps1* script

```
$ .\test.ps1
```
