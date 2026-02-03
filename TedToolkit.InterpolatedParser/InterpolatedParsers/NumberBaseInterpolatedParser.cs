// -----------------------------------------------------------------------
// <copyright file="NumberBaseInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#if NET7_0_OR_GREATER
using System.Globalization;
using System.Numerics;

namespace TedToolkit.InterpolatedParser.InterpolatedParsers;

/// <summary>
/// The string parser.
/// </summary>
/// <typeparam name="T">the type.</typeparam>
public sealed class NumberBaseInterpolatedParser<T> : IInterpolatedParser<T>
    where T : INumberBase<T>
{
    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, ref T result, bool noExceptions)
    {
        var style = InterpolatedParserHelper.GetNumberStyle(format);
        if (noExceptions)
        {
            if (T.TryParse(input, style, CultureInfo.CurrentCulture, out var relay))
            {
                result = relay;
                return ParseResult.Succeed;
            }

            return ParseResult.FailedToParse<T>(input);
        }

        result = T.Parse(input, style, CultureInfo.CurrentCulture);
        return ParseResult.Succeed;
    }
}
#endif