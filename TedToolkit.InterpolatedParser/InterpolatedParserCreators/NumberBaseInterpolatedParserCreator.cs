// -----------------------------------------------------------------------
// <copyright file="NumberBaseInterpolatedParserCreator.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#if NET7_0_OR_GREATER
using System.Diagnostics;
using System.Numerics;

using TedToolkit.InterpolatedParser.InterpolatedParsers;

namespace TedToolkit.InterpolatedParser.InterpolatedParserCreators;

/// <summary>
/// The number base interpolated parser creator.
/// </summary>
public sealed class NumberBaseInterpolatedParserCreator : IInterpolatedParserCreator
{
    /// <inheritdoc />
    public bool CanCreate(Type type)
        => typeof(INumberBase<>).MakeGenericType(type).IsAssignableFrom(type);

    /// <inheritdoc/>
    public IInterpolatedParser Create(Type type)
    {
        return (IInterpolatedParser)
            Activator.CreateInstance(typeof(NumberBaseInterpolatedParser<>)
                .MakeGenericType(type))!;
    }
}
#endif