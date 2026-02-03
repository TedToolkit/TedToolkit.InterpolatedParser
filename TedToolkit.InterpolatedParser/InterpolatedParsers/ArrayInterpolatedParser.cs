// -----------------------------------------------------------------------
// <copyright file="ArrayInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.RegularExpressions;

namespace TedToolkit.InterpolatedParser.InterpolatedParsers;

/// <summary>
/// The string parser.
/// </summary>
/// <typeparam name="T">type.</typeparam>
/// <param name="parser">the item parser.</param>
public sealed class ArrayInterpolatedParser<T>(IInterpolatedParser<T> parser) : IInterpolatedParser<T[]>
{
    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, ref T[] result, bool noExceptions)
    {
        var formatItems = InterpolatedParserHelper.GetFormatItems(format);
        var separator = InterpolatedParserHelper.GetString(formatItems, 0, ",");
        var itemFormat = InterpolatedParserHelper.GetString(formatItems, 1, "");

#if NET9_0_OR_GREATER
        var resultList = new List<T>();
        var parseResults = new List<ParseResult>();
        foreach (var range in input.Split(separator))
        {
            T item = default!;
            parseResults.Add(parser.Parse(input[range], itemFormat, ref item, noExceptions));
            resultList.Add(item);
        }

        result = resultList.ToArray();
        return ParseResult.CreateFailedResults(parseResults);
#elif NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        var items = new string(input).Split(separator);
        result = new T[items.Length];
        var parseResults = new ParseResult[items.Length];
        for (var i = 0; i < result.Length; i++)
            parseResults[i] = parser.Parse(items[i], itemFormat, ref result[i], noExceptions);

        return ParseResult.CreateFailedResults(parseResults);
#else
        var items = Regex.Split(input, Regex.Escape(separator));
        result = new T[items.Length];
        var parseResults = new ParseResult[items.Length];
        for (var i = 0; i < result.Length; i++)
            parseResults[i] = parser.Parse(items[i], itemFormat, ref result[i], noExceptions);

        return ParseResult.CreateFailedResults(parseResults);
#endif
    }
}