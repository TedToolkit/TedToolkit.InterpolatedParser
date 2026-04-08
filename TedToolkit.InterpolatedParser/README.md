# TedToolkit.InterpolatedParser

A .NET library that parses strings using C# interpolated string syntax — extract typed values from text by writing the pattern as an interpolated string.

## Installation

```shell
dotnet add package TedToolkit.InterpolatedParser
```

## Quick Start

```csharp
using TedToolkit.InterpolatedParser;

var name = "";
"I am Ted!".Parse($"I am {name}!");
// name == "Ted"
```

That's it. Write the interpolated string as if you were *formatting* the output, and the library runs the process in reverse to extract the values.

## Usage

### Parsing Strings

```csharp
var name = "";

// Value at the end
"I am Ted".Parse($"I am {name}");

// Value at the start
"Ted is me!".Parse($"{name} is me!");

// Value in the middle
"Hello Ted, welcome!".Parse($"Hello {name}, welcome!");
```

### Parsing Numbers

```csharp
var age = 0;
"I am 18 years old!".Parse($"I am {age} years old!");
// age == 18
```

All primitive numeric types are supported: `int`, `long`, `double`, `float`, `decimal`, `byte`, `short`, `uint`, `ulong`, `ushort`, `sbyte`.

On .NET 7+, any type implementing `INumber<T>` or `IParsable<T>` works automatically.

### Parsing Arrays and Lists

```csharp
// Default separator: comma
string[] names = [];
"There are A,B,C".Parse($"There are {names}");
// names == ["A", "B", "C"]

// Custom separator
string[] names = [];
"There are A;B;C.".Parse($"There are {names:;}.");
// names == ["A", "B", "C"]

// Works with typed arrays too
int[] numbers = [];
"Values: 1,2,3".Parse($"Values: {numbers}");
```

`List<T>` is also supported with the same syntax.

### Parsing Enums

```csharp
MyEnum value = default;
"Status: Active".Parse($"Status: {value}");

// Enum arrays with custom separator
MyEnum[] values = [];
"Items: A;B;C.".Parse($"Items: {values:;}.");
```

### Regex-Based Parsing

For patterns with repetition or flexible matching, use regex syntax:

```csharp
var a = "";
var b = "";
"I am sooooo cool!!Thanks!".ParseRegex($"I am so+ {a}!+{b}");
// a == "cool", b == "Thanks!"
```

### Error Handling

```csharp
// Parse() throws on failure; TryParse() does not
var result = "some input".TryParse($"pattern {value}");

if (result.Type == ParseResultType.SUCCEED)
{
    // value was populated
}

// Regex variants
var result = "input".TryParseRegex($"pattern+ {value}");
```

`ParseResult` contains:
- `Type` — `SUCCEED`, `FAILED_TO_INDEX`, or `FAILED_TO_PARSE`
- `Message` — error details on failure
- `Results` — sub-results for composite parsers

### Custom Parsers

Register your own parser for any type:

```csharp
public class PointParser : IInterpolatedParser<Point>
{
    public ParseResult Parse(ReadOnlySpan<char> input, string format, ref Point result, bool noExceptions)
    {
        // your parsing logic
        return ParseResult.Succeed;
    }
}

InterpolatedParserSettings.AddParser(new PointParser());
```

Or register a parser creator for families of types:

```csharp
public class MyCreator : IInterpolatedParserCreator
{
    public bool CanCreate(Type type) => /* check type */;
    public IInterpolatedParser Create(Type type) => /* create parser */;
}

InterpolatedParserSettings.AddParserCreator(new MyCreator());
```

## Supported Frameworks

| Framework | Versions |
|---|---|
| .NET | 6.0, 7.0, 8.0, 9.0, 10.0 |
| .NET Framework | 4.7.2, 4.8 |
| .NET Standard | 2.0, 2.1 |

## License

LGPL-3.0 — see [COPYING](https://github.com/TedToolkit/TedToolkit.InterpolatedParser/blob/development/COPYING) and [COPYING.LESSER](https://github.com/TedToolkit/TedToolkit.InterpolatedParser/blob/development/COPYING.LESSER).
