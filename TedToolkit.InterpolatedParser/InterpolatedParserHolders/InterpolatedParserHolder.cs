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
internal abstract unsafe class InterpolatedParserHolder<T> :
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

    /// <summary>
    /// Gets the data ref.
    /// </summary>
    protected ref T Ref
        => ref Unsafe.AsRef<T>(_pointer);

    /// <inheritdoc/>
    public abstract ParseResult Parse(
#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        ReadOnlySpan<char> input,
#else
        string input,
#endif
        string format, bool noExceptions);
}