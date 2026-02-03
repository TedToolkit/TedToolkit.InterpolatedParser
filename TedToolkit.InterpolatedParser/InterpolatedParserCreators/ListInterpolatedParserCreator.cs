// -----------------------------------------------------------------------
// <copyright file="ListInterpolatedParserCreator.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.InterpolatedParser.InterpolatedParsers;

namespace TedToolkit.InterpolatedParser.InterpolatedParserCreators;
#pragma warning disable CA1062

/// <summary>
/// The list interpolated parser creator.
/// </summary>
public sealed class ListInterpolatedParserCreator : IInterpolatedParserCreator
{
    /// <inheritdoc />
    public bool CanCreate(Type type)
    {
        if (!type.IsGenericType)
            return false;

        return type.GetGenericTypeDefinition() == typeof(List<>);
    }

    /// <inheritdoc />
    public IInterpolatedParser Create(Type type)
    {
        var itemType = type.GetGenericArguments()[0];
        var parser = InterpolatedParserSettings.GetParser(itemType);

        return (IInterpolatedParser)
            Activator.CreateInstance(typeof(ListInterpolatedParser<>)
                .MakeGenericType(itemType), parser)!;
    }
}