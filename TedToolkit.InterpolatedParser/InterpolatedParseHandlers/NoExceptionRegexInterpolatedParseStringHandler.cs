// -----------------------------------------------------------------------
// <copyright file="NoExceptionRegexInterpolatedParseStringHandler.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The default handler for the interpolated parser.
/// </summary>
[InterpolatedStringHandler]
public ref struct NoExceptionRegexInterpolatedParseStringHandler
{
    private MainInterpolatedParser _parser;

    /// <summary>
    /// Initializes a new instance of the <see cref="NoExceptionRegexInterpolatedParseStringHandler"/> struct.
    /// Create a handler.
    /// </summary>
    /// <param name="literalLength">the literal length.</param>
    /// <param name="formattedCount">the formated count.</param>
    /// <param name="input">the input string.</param>
#pragma warning disable RCS1163
    public NoExceptionRegexInterpolatedParseStringHandler(int literalLength, int formattedCount, string input)
#pragma warning restore RCS1163
    {
        _parser = new(formattedCount, input, true);
    }

    /// <summary>
    /// Add the literal.
    /// </summary>
    /// <param name="s">string.</param>
    /// <exception cref="ArgumentNullException">s is null.</exception>
    public void AppendLiteral(
        [StringSyntax(StringSyntaxAttribute.Regex)]
        string s)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(s);
#else
        if (s is null)
            throw new ArgumentNullException(nameof(s));
#endif

        var regexResult = new Regex(s).Match(_parser.Input, _parser.Start);
        var succeed = regexResult.Success;
        _parser.AppendLiteral(succeed ? regexResult.Index : -1, regexResult.Length, s);
    }

    /// <summary>
    /// Append the formatted.
    /// </summary>
    /// <param name="t">result.</param>
    /// <param name="format">format.</param>
    /// <typeparam name="T">type.</typeparam>
    public void AppendFormatted<T>(in T t, string format = "")
        => _parser.AppendFormatted(in t, format);

    /// <summary>
    /// Solve.
    /// </summary>
    /// <returns>result.</returns>
    internal ParseResult Solve()
        => _parser.Solve();
}