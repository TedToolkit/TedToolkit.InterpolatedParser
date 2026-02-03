using System.Text.RegularExpressions;

using BenchmarkDotNet.Attributes;

namespace TedToolkit.InterpolatedParser.Benchmark;

[MemoryDiagnoser]
public partial class ParseRunner
{
    private static readonly string _input = "Hi, I am Ted. And I'm 18 years old.";

    [Benchmark]
    public void InterpolatedParser()
    {
        var name = "";
        var age = 0;
        InterpolatedParsing.InterpolatedParser.Parse(_input,
            $"Hi, I am {name}. And I'm {age} years old.");
    }

    [Benchmark]
    public void TedToolkit()
    {
        var name = "";
        var age = 0;
        _input.Parse($"Hi, I am {name}. And I'm {age} years old.");
    }

    [Benchmark]
    public void Regex()
    {
        var partern = GetInfoRegex();
        var matches = partern.Matches(_input);
        var name = matches[0].Groups[1].Value;
        var age = int.Parse(matches[0].Groups[2].Value);
    }

    [GeneratedRegex(@"I am (\w+)\. And I'm (\d+) years old\.")]
    private static partial Regex GetInfoRegex();
}