// -----------------------------------------------------------------------
// <copyright file="IInterpolatedParserHolder.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The holder.
/// </summary>
internal interface IInterpolatedParserHolder
{
    /// <summary>
    /// Parse the value.
    /// </summary>
    /// <param name="input">the input.</param>
    /// <param name="format">format.</param>
    /// <param name="noExceptions">is trying.</param>
    /// <returns>result.</returns>
    ParseResult Parse(
        #if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        ReadOnlySpan<char> input,
        #else
        string input,
        #endif
        string format, bool noExceptions);
}