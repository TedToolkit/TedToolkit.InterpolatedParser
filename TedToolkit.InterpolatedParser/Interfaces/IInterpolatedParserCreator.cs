// -----------------------------------------------------------------------
// <copyright file="IInterpolatedParserCreator.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The Interpolated parser creator.
/// </summary>
public interface IInterpolatedParserCreator
{
    /// <summary>
    /// Can create from the ype.
    /// </summary>
    /// <param name="type">type.</param>
    /// <returns>result.</returns>
    bool CanCreate(Type type);

    /// <summary>
    /// Create one.
    /// </summary>
    /// <param name="type">type.</param>
    /// <returns>parser.</returns>
    IInterpolatedParser Create(Type type);
}