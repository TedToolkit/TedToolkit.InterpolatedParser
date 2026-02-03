// -----------------------------------------------------------------------
// <copyright file="EnumInterpolatedParserCreator.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

using TedToolkit.InterpolatedParser.InterpolatedParsers;

namespace TedToolkit.InterpolatedParser.InterpolatedParserCreators;

/// <summary>
/// The enum interpolated parser creator.
/// </summary>
public sealed class EnumInterpolatedParserCreator : IInterpolatedParserCreator
{
    /// <inheritdoc/>
    public bool CanCreate(Type type)
        => type.IsEnum;

    /// <inheritdoc/>
    public IInterpolatedParser Create(Type type)
    {
        return (IInterpolatedParser)
            Activator.CreateInstance(typeof(EnumInterpolatedParser<>)
                .MakeGenericType(type))!;
    }
}