// -----------------------------------------------------------------------
// <copyright file="InterpolatedParserItemHolder.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The parser holder for values.
/// </summary>
/// <param name="parser">parser.</param>
/// <typeparam name="T">the type.</typeparam>
internal sealed class InterpolatedParserItemHolder<T>(IInterpolatedParser<T> parser) :
    InterpolatedParserHolder<T>
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
        return parser.Parse(input, format, ref Ref, noExceptions);
    }
}