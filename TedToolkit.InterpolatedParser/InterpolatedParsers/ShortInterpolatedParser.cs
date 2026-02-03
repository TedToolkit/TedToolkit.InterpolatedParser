// -----------------------------------------------------------------------
// <copyright file="ShortInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#if !NET7_0_OR_GREATER
using System.Globalization;

namespace TedToolkit.InterpolatedParser.InterpolatedParsers;

/// <summary>
/// The string parser.
/// </summary>
public sealed class ShortInterpolatedParser : IInterpolatedParser<short>
{
    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, ref short result, bool noExceptions)
    {
        var style = InterpolatedParserHelper.GetNumberStyle(format);
        if (noExceptions)
        {
            if (short.TryParse(input, style, CultureInfo.CurrentCulture, out var relay))
            {
                result = relay;
                return ParseResult.Succeed;
            }

            return ParseResult.FailedToParse<short>(input);
        }

        result = short.Parse(input, style, CultureInfo.CurrentCulture);
        return ParseResult.Succeed;
    }
}
#endif