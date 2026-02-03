// -----------------------------------------------------------------------
// <copyright file="IntInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Globalization;

namespace TedToolkit.InterpolatedParser.InterpolatedParsers;

/// <summary>
/// The string parser.
/// </summary>
public sealed class IntInterpolatedParser : IInterpolatedParser<int>
{
    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, ref int result, bool noExceptions)
    {
        var style = InterpolatedParserHelper.GetNumberStyle(format);
        if (noExceptions)
        {
            if (int.TryParse(input, style, CultureInfo.CurrentCulture, out var relay))
            {
                result = relay;
                return ParseResult.Succeed;
            }

            return ParseResult.FailedToParse<int>(input);
        }

        result = int.Parse(input, style, CultureInfo.CurrentCulture);
        return ParseResult.Succeed;
    }
}