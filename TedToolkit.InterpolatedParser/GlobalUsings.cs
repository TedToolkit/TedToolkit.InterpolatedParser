// -----------------------------------------------------------------------
// <copyright file="GlobalUsings.cs" company="TedToolkit">
// Copyright (c) TedToolkit. All rights reserved.
// Licensed under the LGPL-3.0 license. See COPYING, COPYING.LESSER file in the project root for full license information.
// </copyright>
// -----------------------------------------------------------------------

#pragma warning disable RCS1056

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
global using StringPart = System.ReadOnlySpan<char>;
#else
global using StringPart = string;
#endif