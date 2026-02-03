// -----------------------------------------------------------------------
// <copyright file="InterpolatedParserHolder.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.CompilerServices;

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The parser holder for values.
/// </summary>
/// <typeparam name="T">the type.</typeparam>
/// <param name="parser">parser.</param>
internal sealed unsafe class InterpolatedParserHolder<T>(IInterpolatedParser<T> parser) :
    IInterpolatedParserHolder
{
    private void* _pointer;

    /// <summary>
    /// Set the value.
    /// </summary>
    /// <param name="value">value.</param>
    public void SetValue(in T value)
    {
#if NET10_0_OR_GREATER
        _pointer = Unsafe.AsPointer(in value);
#else
        _pointer = Unsafe.AsPointer(ref Unsafe.AsRef(in value));
#endif
    }

    /// <inheritdoc/>
    public ParseResult Parse(StringPart input, string format, bool noExceptions)
        => parser.Parse(input, format, ref Unsafe.AsRef<T>(_pointer), noExceptions);
}