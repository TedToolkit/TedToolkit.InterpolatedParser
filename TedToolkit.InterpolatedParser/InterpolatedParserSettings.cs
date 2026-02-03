// -----------------------------------------------------------------------
// <copyright file="InterpolatedParserSettings.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.InteropServices;

using TedToolkit.InterpolatedParser.InterpolatedParserCreators;
using TedToolkit.InterpolatedParser.InterpolatedParsers;

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The settings for the interpolated parsers.
/// </summary>
public static class InterpolatedParserSettings
{
    private static readonly Dictionary<Type, IInterpolatedParser> _parsers = [];

    private static readonly List<IInterpolatedParserCreator>
        _interpolatedParserCreators = [];

    [ThreadStatic]
    private static Dictionary<Type, IInterpolatedParserHolder>? _holders;

    static InterpolatedParserSettings()
    {
        AddParser(new StringInterpolatedParser());
#if !NET7_0_OR_GREATER
        AddParser(new IntInterpolatedParser());
        AddParser(new ByteInterpolatedParser());
        AddParser(new DecimalInterpolatedParser());
        AddParser(new DoubleInterpolatedParser());
        AddParser(new FloatInterpolatedParser());
        AddParser(new LongInterpolatedParser());
        AddParser(new SbyteInterpolatedParser());
        AddParser(new ShortInterpolatedParser());
        AddParser(new UintInterpolatedParser());
        AddParser(new UlongInterpolatedParser());
        AddParser(new UshortInterpolatedParser());
#endif

        AddParserCreator(new ListInterpolatedParserCreator());
        AddParserCreator(new ArrayInterpolatedParserCreator());
        AddParserCreator(new EnumInterpolatedParserCreator());
#if NET7_0_OR_GREATER
        AddParserCreator(new NumberBaseInterpolatedParserCreator());
        AddParserCreator(new ParsableInterpolatedParserCreator());
#endif
    }

    /// <summary>
    /// Add a parser for it.
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <typeparam name="T">target type.</typeparam>
    public static void AddParser<T>(IInterpolatedParser<T> parser)
        => _parsers[typeof(T)] = parser;

    /// <summary>
    /// Clear all parsers.
    /// </summary>
    public static void ClearParsers()
        => _parsers.Clear();

    /// <summary>
    /// Add the predicate creator.
    /// </summary>
    /// <param name="creator">creator.</param>
    public static void AddParserCreator(IInterpolatedParserCreator creator)
        => _interpolatedParserCreators.Add(creator);

    /// <summary>
    /// Clear all holders.
    /// </summary>
    public static void ClearHolders()
        => _holders?.Clear();

    /// <summary>
    /// Clear all creators.
    /// </summary>
    public static void ClearCreators()
        => _interpolatedParserCreators.Clear();

    /// <summary>
    /// Get the parser from the type.
    /// </summary>
    /// <typeparam name="T">type.</typeparam>
    /// <returns>parser.</returns>
    /// <exception cref="KeyNotFoundException">can't find the parser.</exception>
    public static IInterpolatedParser<T> GetParser<T>()
        => (IInterpolatedParser<T>)GetParser(typeof(T));

    /// <summary>
    /// Get the parser from the type.
    /// </summary>
    /// <param name="type">type.</param>
    /// <returns>parser.</returns>
    /// <exception cref="KeyNotFoundException">can't find the parser.</exception>
    /// <exception cref="ArgumentNullException">type is null.</exception>
    public static IInterpolatedParser GetParser(Type type)
    {
#if NET6_0_OR_GREATER
        ArgumentNullException.ThrowIfNull(type);
#else
        if (type is null)
            throw new ArgumentNullException(nameof(type));
#endif
#if NET6_0_OR_GREATER
        ref var parser = ref CollectionsMarshal.GetValueRefOrAddDefault(_parsers, type, out var exists);
        if (exists && parser is not null)
#else
        if (_parsers.TryGetValue(type, out var parser))
#endif
            return parser;

        foreach (var creator in _interpolatedParserCreators)
        {
            if (!creator.CanCreate(type))
                continue;

            var addedParser = creator.Create(type);

#if NET6_0_OR_GREATER
            parser = addedParser;
#else
            _parsers[type] = addedParser;
#endif
            return addedParser;
        }

        throw new KeyNotFoundException(Localization.CantFindParser(type.FullName));
    }

    /// <summary>
    /// Get the older.
    /// </summary>
    /// <typeparam name="T">type.</typeparam>
    /// <returns>result.</returns>
    internal static InterpolatedParserHolder<T> GetHolder<T>()
    {
        _holders ??= [];

        var type = typeof(T);
#if NET6_0_OR_GREATER
        ref var holder = ref CollectionsMarshal.GetValueRefOrAddDefault(_holders, type, out var exists);
        if (exists && holder is not null)
#else
        if (_holders.TryGetValue(type, out var holder))
#endif
            return (InterpolatedParserHolder<T>)holder;

        var addedHolder = new InterpolatedParserHolder<T>(GetParser<T>());
#if NET6_0_OR_GREATER
        holder = addedHolder;
#else
        _holders[type] = addedHolder;
#endif

        return addedHolder;
    }
}