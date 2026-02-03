// -----------------------------------------------------------------------
// <copyright file="InterpolatedParserArrayHolder.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The parser holder for values.
/// </summary>
/// <param name="parser">parser.</param>
/// <typeparam name="T">the type.</typeparam>
internal sealed class InterpolatedParserArrayHolder<T>(IInterpolatedParser<T> parser) :
    InterpolatedParserHolder<T[]>
{
    /// <inheritdoc/>
    public override ParseResult Parse(
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        ReadOnlySpan<char> input,
#else
        string input,
#endif
        string format, bool noExceptions)
    {
        var formatItems = format.Split('|');
        var separator = InterpolatedParserHolderHelper.GetString(formatItems, 0, ",");
        var itemFormat = InterpolatedParserHolderHelper.GetString(formatItems, 1, "");

#if NET9_0_OR_GREATER
        var result = new List<T>();
        var parseResults = new List<ParseResult>();
        foreach (var range in input.Split(separator))
        {
            T item = default!;
            parseResults.Add(parser.Parse(input[range], itemFormat, ref item, noExceptions));
            result.Add(item);
        }

        Ref = result.ToArray();
        return ParseResult.CreateFailedResults(parseResults);
#elif NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        var items = new string(input).Split(separator);
        var result = new T[items.Length];
        var parseResults = new ParseResult[items.Length];
        for (var i = 0; i < result.Length; i++)
            parseResults[i] = parser.Parse(items[i], itemFormat, ref result[i], noExceptions);

        Ref = result;
        return ParseResult.CreateFailedResults(parseResults);
#else
        var items = Regex.Split(input, Regex.Escape(separator));
        var result = new T[items.Length];
        var parseResults = new ParseResult[items.Length];
        for (var i = 0; i < result.Length; i++)
            parseResults[i] = parser.Parse(items[i], itemFormat, ref result[i], noExceptions);

        Ref = result;
        return ParseResult.CreateFailedResults(parseResults);
#endif
    }
}