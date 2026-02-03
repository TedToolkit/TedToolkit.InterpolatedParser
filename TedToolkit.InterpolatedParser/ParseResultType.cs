// -----------------------------------------------------------------------
// <copyright file="ParseResultType.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

namespace TedToolkit.InterpolatedParser;

/// <summary>
/// Parse result type.
/// </summary>
public enum ParseResultType
{
    /// <summary>
    /// Unknown.
    /// </summary>
    UNKNOWN = 0,

    /// <summary>
    /// Succeed.
    /// </summary>
    SUCCEED = 1,

    /// <summary>
    /// Failed to index.
    /// </summary>
    FAILED_TO_INDEX = 2,

    /// <summary>
    /// Failed to parse by the user.
    /// </summary>
    FAILED_TO_PARSE = 3,
}