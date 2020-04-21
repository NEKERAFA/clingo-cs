# Clingo# (ClingoSharp)

[![.NET Standard v2.0](https://img.shields.io/badge/.NET%20Standard-2.0-7014e8)](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)
[![Clingo 5.4.0](https://img.shields.io/badge/Clingo-5.4.0-blue)](https://github.com/potassco/clingo)
![ClingoSharp Workflow](https://github.com/NEKERAFA/ClingoSharp/workflows/ClingoSharp%20Workflow/badge.svg?branch=master&event=push)
[![Coverage Status](https://coveralls.io/repos/github/NEKERAFA/ClingoSharp/badge.svg?branch=master)](https://coveralls.io/github/NEKERAFA/ClingoSharp?branch=master)

A C# bindings to the [clingo](https://github.com/potassco/clingo) library.

## Requirements

* Windows 10 or Ubuntu 18.04
* [.NET Core v3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1) or [.NET Framework v4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)
* Visual Studio 19 or GCC, and CMake

## Development

Clingo# uses the clingo Python API as reference. You can see the Clingo# API reference is hosted on https://nekerafa.github.io/ClingoSharp/api/index.html

### Packages

#### ClingoSharp

*Working in progress...*

#### ClingoSharp.CoreServices

*Working in progress...*

#### ClingoSharp.NativeWrapper

*Working in progress...*

## Build

Clone the repository and the submodules:

```
$ git clone --recurse-submodules -j8 git://github.com/NEKERAFA/ClingoSharp.git
```

Execute the build script (*.ps1* in Windows or *.sh* in GNU/Linux) to build clingo:

```
$ .\build.ps1
```

Compile the .NET project

```
$ donet --configuration Debug .\ClingoSharp.sln
```

## Test

With the project built, you can execute the *test.ps1* script

```
$ .\test.ps1
```
