// -----------------------------------------------------------------------
// <copyright file="RegexCache.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// Regex cache.
/// </summary>
internal static class RegexCache
{
    private static readonly ConcurrentDictionary<string, Regex> _regexes = [];

    /// <summary>
    /// Get the regex.
    /// </summary>
    /// <param name="pattern">pattern.</param>
    /// <returns>regex.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static Regex Get(string pattern)
        => _regexes.GetOrAdd(pattern, static p => new Regex(p, RegexOptions.Compiled));
}