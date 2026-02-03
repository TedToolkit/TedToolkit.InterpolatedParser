// -----------------------------------------------------------------------
// <copyright file="StringInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The string parser.
/// </summary>
internal sealed class StringInterpolatedParser : IInterpolatedParser<string>
{
    /// <inheritdoc/>
    public ParseResult Parse(
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        ReadOnlySpan<char> input,
#else
        string input,
#endif
        string format, ref string result, bool noExceptions)
    {
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        result = new(input);

#else
        result = input;
#endif
        return default;
    }
}