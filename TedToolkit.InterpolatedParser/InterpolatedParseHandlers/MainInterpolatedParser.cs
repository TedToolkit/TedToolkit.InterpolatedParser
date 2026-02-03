// -----------------------------------------------------------------------
// <copyright file="MainInterpolatedParser.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The main parser.
/// </summary>
internal ref struct MainInterpolatedParser
{
    private readonly ParseResult[] _results;

    /// <summary>
    /// The input string.
    /// </summary>
    internal readonly string Input;

    private readonly bool _noExceptions;

    private IInterpolatedParserHolder? _holder;

    private int _index;

    /// <summary>
    /// The start index.
    /// </summary>
    internal int Start;

    private string _format;

    /// <summary>
    /// Initializes a new instance of the <see cref="MainInterpolatedParser"/> struct.
    /// Create a handler.
    /// </summary>
    /// <param name="formattedCount">the formated count.</param>
    /// <param name="input">the input string.</param>
    /// <param name="noExceptions">is try.</param>
#pragma warning disable RCS1163
    internal MainInterpolatedParser(int formattedCount, string input, bool noExceptions)
#pragma warning restore RCS1163
    {
        _results = new ParseResult[formattedCount];
        Input = input;
        _noExceptions = noExceptions;
        _format = "";
    }

    /// <summary>
    /// Append the literal.
    /// </summary>
    /// <param name="start">start index.</param>
    /// <param name="length">length.</param>
    /// <param name="key">key.</param>
    public void AppendLiteral(int start, int length, string key)
    {
        try
        {
            if (start < 0)
            {
                if (_holder is not null)
                    _results[_index++] = ParseResult.CreateFailedIndexResult(key, Input.Substring(Start));

                return;
            }
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            var text = Input.AsSpan(Start, start - Start);
#else
            var text = Input.Substring(Start, start - Start);
#endif
            if (_holder is not null)
                _results[_index++] = _holder.Parse(text, _format, _noExceptions);
        }
        finally
        {
            Start = start + length;
            _holder = null;
        }
    }

    /// <summary>
    /// Append something formatted.
    /// </summary>
    /// <param name="t">thing.</param>
    /// <param name="format">format.</param>
    /// <typeparam name="T">type.</typeparam>
    public void AppendFormatted<T>(in T t, string format)
    {
        var holder = InterpolatedParserSettings.GetHolder<T>();
        holder.SetValue(t);
        _holder = holder;
        _format = format;
    }

    /// <summary>
    /// Solve the result.
    /// </summary>
    /// <returns>result.</returns>
    public ParseResult[] Solve()
    {
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        var text = Input.AsSpan(Start);
#else
        var text = Input.Substring(Start);
#endif
        if (_holder is not null)
            _results[_index++] = _holder.Parse(text, _format, _noExceptions);

        return _results;
    }
}