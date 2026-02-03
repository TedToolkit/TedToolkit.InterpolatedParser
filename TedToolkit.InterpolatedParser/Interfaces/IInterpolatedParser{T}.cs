// -----------------------------------------------------------------------
// <copyright file="IInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The Interpolated parser item.
/// </summary>
/// <typeparam name="T">the type.</typeparam>
public interface IInterpolatedParser<T> : IInterpolatedParser
{
    /// <summary>
    /// Parse one item.
    /// </summary>
    /// <param name="input">the input string.</param>
    /// <param name="format">format string.</param>
    /// <param name="result">result.</param>
    /// <param name="noExceptions">true for no exceptions.</param>
    /// <returns>succeed.</returns>
    ParseResult Parse(StringPart input, string format, ref T result, bool noExceptions);
}