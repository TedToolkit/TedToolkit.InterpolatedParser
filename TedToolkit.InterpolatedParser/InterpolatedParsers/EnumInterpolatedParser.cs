// -----------------------------------------------------------------------
// <copyright file="EnumInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser.InterpolatedParsers;

/// <summary>
/// The enum interpolated parser.
/// </summary>
/// <typeparam name="T">enum type.</typeparam>
public sealed class EnumInterpolatedParser<T> : IInterpolatedParser<T>
    where T : struct, Enum
{
    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, ref T result, bool noExceptions)
    {
        if (noExceptions)
        {
            if (Enum.TryParse<T>(
#if NETSTANDARD2_1
                    new(input),
#else
                    input,
#endif
                    out var value))
            {
                result = value;
                return ParseResult.Succeed;
            }

            return ParseResult.FailedToParse<T>(input);
        }

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            result = Enum.Parse<T>(
#if NETSTANDARD2_1
            new(input));
#else
            input);
#endif
#else
        result = (T)Enum.Parse(typeof(T), input);
#endif

        return ParseResult.Succeed;
    }
}