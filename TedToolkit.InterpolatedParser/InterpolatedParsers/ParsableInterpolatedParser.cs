// -----------------------------------------------------------------------
// <copyright file="ParsableInterpolatedParser.cs" company="TedToolkit">
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
public sealed class ParsableInterpolatedParser<T> : IInterpolatedParser<T>
    where T : IParsable<T>
{
    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, ref T result, bool noExceptions)
    {
        if (noExceptions)
        {
            if (T.TryParse(
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
                    new(input),
#else
                    input,
#endif
                    CultureInfo.CurrentCulture, out var relay))
            {
                result = relay;
                return ParseResult.Succeed;
            }

            return ParseResult.FailedToParse<T>(input);
        }

        result = T.Parse(
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            new(input),
#else
            input,
#endif
            CultureInfo.CurrentCulture);
        return ParseResult.Succeed;
    }
}
#endif