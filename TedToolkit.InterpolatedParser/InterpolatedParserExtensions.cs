// -----------------------------------------------------------------------
// <copyright file="InterpolatedParserExtensions.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace TedToolkit.InterpolatedParser;
#pragma warning disable IDE0060, CS8947, RCS1175, RCS1163

/// <summary>
/// the extensions for the interpolated parser.
/// </summary>
public static class InterpolatedParserExtensions
{
    /// <summary>
    ///     The basic Parse.
    /// </summary>
    /// <param name="input">input string.</param>
    /// <param name="template">template.</param>
    /// <returns>results.</returns>
    public static IReadOnlyList<ParseResult> Parse(
        this string input,
        [InterpolatedStringHandlerArgument(nameof(input))]
        InterpolatedParseStringHandler template)
    {
        return template.Solve();
    }
}