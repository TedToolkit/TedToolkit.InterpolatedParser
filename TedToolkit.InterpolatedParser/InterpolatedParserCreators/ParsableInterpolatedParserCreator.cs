// -----------------------------------------------------------------------
// <copyright file="ParsableInterpolatedParserCreator.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#if NET7_0_OR_GREATER

using TedToolkit.InterpolatedParser.InterpolatedParsers;

namespace TedToolkit.InterpolatedParser.InterpolatedParserCreators;

/// <summary>
/// The number base interpolated parser creator.
/// </summary>
public sealed class ParsableInterpolatedParserCreator : IInterpolatedParserCreator
{
    /// <inheritdoc />
    public bool CanCreate(Type type)
        => typeof(IParsable<>).MakeGenericType(type).IsAssignableFrom(type);

    /// <inheritdoc/>
    public IInterpolatedParser Create(Type type)
    {
        return (IInterpolatedParser)
            Activator.CreateInstance(typeof(ParsableInterpolatedParser<>)
                .MakeGenericType(type))!;
    }
}
#endif