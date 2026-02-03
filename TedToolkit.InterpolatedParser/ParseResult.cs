// -----------------------------------------------------------------------
// <copyright file="ParseResult.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// Parse result.
/// </summary>
public readonly record struct ParseResult
{
    /// <summary>
    /// Gets the result type.
    /// </summary>
    public ParseResultType Type { get; }

    /// <summary>
    /// Gets the message.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Gets sub results.
    /// </summary>
    public IReadOnlyList<ParseResult> Results { get; }

    private ParseResult(ParseResultType type, string message, IReadOnlyList<ParseResult> results)
    {
        Type = type;
        Message = message;
        Results = results;
    }

    /// <summary>
    /// Create from the failed index.
    /// </summary>
    /// <param name="key">key.</param>
    /// <param name="str">string.</param>
    /// <returns>result.</returns>
    internal static ParseResult CreateFailedIndexResult(string key, string str)
    {
        return new(ParseResultType.FAILED_TO_INDEX,
            Localization.FailedIndex(key, str), []);
    }

    /// <summary>
    /// Create form the failed results.
    /// </summary>
    /// <param name="results">results.</param>
    /// <returns>result.</returns>
    internal static ParseResult CreateFailedResults(IReadOnlyList<ParseResult> results)
        => new(results.Min(i => i.Type), "", results);

    /// <summary>
    /// Gets succeed result.
    /// </summary>
    public static ParseResult Succeed { get; } = new(ParseResultType.SUCCEED, "", []);

    /// <summary>
    /// Create failed result.
    /// </summary>
    /// <param name="message">message.</param>
    /// <returns>result.</returns>
    public static ParseResult Failed(string message)
        => new(ParseResultType.FAILED_TO_PARSE, message, []);
}