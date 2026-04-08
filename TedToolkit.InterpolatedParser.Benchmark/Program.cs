// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;

using TedToolkit.InterpolatedParser.Benchmark;

Console.WriteLine("Hello, World!");

BenchmarkRunner.Run<ParseRunner>();