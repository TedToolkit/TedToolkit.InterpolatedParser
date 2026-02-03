// -----------------------------------------------------------------------
// <copyright file="InterpolatedParserSettings.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using System.Runtime.InteropServices;

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The settings for the interpolated parsers.
/// </summary>
public static class InterpolatedParserSettings
{
    private static readonly Dictionary<Type, object> _parsers = [];

    [ThreadStatic]
    private static Dictionary<Type, IInterpolatedParserHolder>? _itemHolders;

    [ThreadStatic]
    private static Dictionary<Type, IInterpolatedParserHolder>? _listHolders;

    [ThreadStatic]
    private static Dictionary<Type, IInterpolatedParserHolder>? _arrayHolders;

    static InterpolatedParserSettings()
    {
        AddParser(new StringInterpolatedParser());
    }

    /// <summary>
    /// Add a parser for it.
    /// </summary>
    /// <param name="parser">parser.</param>
    /// <typeparam name="T">target type.</typeparam>
    public static void AddParser<T>(IInterpolatedParser<T> parser)
        => _parsers[typeof(T)] = parser;

    /// <summary>
    /// Get the item holders.
    /// </summary>
    /// <typeparam name="T">type.</typeparam>
    /// <returns>result.</returns>
    internal static InterpolatedParserItemHolder<T> GetItemHolder<T>()
    {
        return (InterpolatedParserItemHolder<T>)GetHolder<T>(ref _itemHolders,
            static i => new InterpolatedParserItemHolder<T>(i));
    }

    /// <summary>
    /// Get the list holders.
    /// </summary>
    /// <typeparam name="T">type.</typeparam>
    /// <returns>result.</returns>
    internal static InterpolatedParserListHolder<T> GetListHolder<T>()
    {
        return (InterpolatedParserListHolder<T>)GetHolder<T>(ref _listHolders,
            static i => new InterpolatedParserListHolder<T>(i));
    }

    /// <summary>
    /// Get the array holders.
    /// </summary>
    /// <typeparam name="T">type.</typeparam>
    /// <returns>result.</returns>
    internal static InterpolatedParserArrayHolder<T> GetArrayHolder<T>()
    {
        return (InterpolatedParserArrayHolder<T>)GetHolder<T>(ref _arrayHolders,
            static i => new InterpolatedParserArrayHolder<T>(i));
    }

    private static IInterpolatedParserHolder GetHolder<T>(ref Dictionary<Type, IInterpolatedParserHolder>? holders,
        Func<IInterpolatedParser<T>, IInterpolatedParserHolder> creator)
    {
        holders ??= [];

        var type = typeof(T);
#if NET6_0_OR_GREATER
        ref var holder = ref CollectionsMarshal.GetValueRefOrAddDefault(holders, type, out var exists);
        if (exists && holder is not null)
#else
        if (holders.TryGetValue(type, out var holder))
#endif
            return holder;

        if (!_parsers.TryGetValue(type, out var parser) || parser is not IInterpolatedParser<T> typedParser)
            throw new KeyNotFoundException(Localization.CantFindParser(type.FullName));

        var addedHolder = creator(typedParser);

#if NET6_0_OR_GREATER
        holder = addedHolder;
#else
        holders[type] = addedHolder;
#endif

        return addedHolder;
    }
}