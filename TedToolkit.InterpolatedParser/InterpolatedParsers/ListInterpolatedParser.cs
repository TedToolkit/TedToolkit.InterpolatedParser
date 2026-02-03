// -----------------------------------------------------------------------
// <copyright file="ListInterpolatedParser.cs" company="TedToolkit">
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
public sealed class ListInterpolatedParser<T>(IInterpolatedParser<T> parser) : IInterpolatedParser<List<T>>
{
    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, ref List<T> result, bool noExceptions)
    {
        var formatItems = InterpolatedParserHelper.GetFormatItems(format);
        var separator = InterpolatedParserHelper.GetString(formatItems, 0, ",");
        var itemFormat = InterpolatedParserHelper.GetString(formatItems, 1, "");

#if NET9_0_OR_GREATER
        result = [];
        var parseResults = new List<ParseResult>();
        foreach (var range in input.Split(separator))
        {
            T item = default!;
            parseResults.Add(parser.Parse(input[range], itemFormat, ref item, noExceptions));
            result.Add(item);
        }

        return ParseResult.CreateFailedResults(parseResults);
#elif NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        var items = new string(input).Split(separator);
        var resultArray = new T[items.Length];
        var parseResults = new ParseResult[items.Length];
        for (var i = 0; i < resultArray.Length; i++)
            parseResults[i] = parser.Parse(items[i], itemFormat, ref resultArray[i], noExceptions);

        result = resultArray.ToList();
        return ParseResult.CreateFailedResults(parseResults);
#else
        var items = Regex.Split(input, Regex.Escape(separator));
        var resultArray = new T[items.Length];
        var parseResults = new ParseResult[items.Length];
        for (var i = 0; i < resultArray.Length; i++)
            parseResults[i] = parser.Parse(items[i], itemFormat, ref resultArray[i], noExceptions);

        result = resultArray.ToList();
        return ParseResult.CreateFailedResults(parseResults);
#endif
    }
}