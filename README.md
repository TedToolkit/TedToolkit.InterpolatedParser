# TedToolkit.InterpolatedParser

[![NuGet](https://img.shields.io/nuget/v/TedToolkit.InterpolatedParser)](https://www.nuget.org/packages/TedToolkit.InterpolatedParser)
[![License: LGPL-3.0](https://img.shields.io/badge/License-LGPL--3.0-blue.svg)](COPYING.LESSER)

A .NET library that parses strings using C# interpolated string syntax — extract typed values from text by writing the pattern as an interpolated string.

```csharp
var name = "";
var age = 0;
"I am Ted and I am 18 years old!".Parse($"I am {name} and I am {age} years old!");
// name == "Ted", age == 18
```

## Features

- **Intuitive syntax** — write the pattern as if you were formatting the string, the library reverses the process
- **Type-safe extraction** — parses directly into `int`, `double`, `string`, enums, arrays, lists, and more
- **Regex support** — `ParseRegex` / `TryParseRegex` for patterns with repetition or flexible matching
- **Extensible** — register custom `IInterpolatedParser<T>` implementations for any type
- **Broad framework support** — .NET 6–10, .NET Framework 4.7.2+, .NET Standard 2.0+
- **High performance** — uses `ref struct` interpolated string handlers, `ReadOnlySpan<char>`, and cached regex instances

## Installation

```shell
dotnet add package TedToolkit.InterpolatedParser
```

## Quick Examples

### Strings and Numbers

```csharp
using TedToolkit.InterpolatedParser;

var name = "";
"I am Ted!".Parse($"I am {name}!");

var age = 0;
"I am 18 years old!".Parse($"I am {age} years old!");
```

### Arrays with Custom Separators

```csharp
string[] names = [];
"There are A,B,C".Parse($"There are {names}");
// names == ["A", "B", "C"]

string[] names = [];
"There are A;B;C.".Parse($"There are {names:;}.");
// names == ["A", "B", "C"]  (semicolon separator)
```

### Enums

```csharp
MyEnum[] values = [];
"Items: A;B;C.".Parse($"Items: {values:;}.");
```

### Regex Patterns

```csharp
var a = "";
var b = "";
"I am sooooo cool!!Thanks!".ParseRegex($"I am so+ {a}!+{b}");
// a == "cool", b == "Thanks!"
```

### Error Handling

```csharp
var result = "input".TryParse($"pattern {value}");
if (result.Type == ParseResultType.SUCCEED) { /* ... */ }
```

## API Reference

| Method | Description |
|---|---|
| `string.Parse(...)` | Parse with literal matching; throws on failure |
| `string.TryParse(...)` | Parse with literal matching; returns result without throwing |
| `string.ParseRegex(...)` | Parse with regex matching; throws on failure |
| `string.TryParseRegex(...)` | Parse with regex matching; returns result without throwing |

## Supported Frameworks

| Framework | Versions |
|---|---|
| .NET | 6.0, 7.0, 8.0, 9.0, 10.0 |
| .NET Framework | 4.7.2, 4.8 |
| .NET Standard | 2.0, 2.1 |

On .NET 7+, any type implementing `INumber<T>` or `IParsable<T>` is automatically supported.

## Project Structure

```
TedToolkit.InterpolatedParser/          # Main library (NuGet package)
TedToolkit.InterpolatedParser.Tests/    # Unit tests (TUnit)
TedToolkit.InterpolatedParser.Benchmark/# Performance benchmarks (BenchmarkDotNet)
Build/                                  # CI/CD pipeline (ModularPipelines)
```

## License

This project is licensed under the [GNU Lesser General Public License v3.0](COPYING.LESSER).
