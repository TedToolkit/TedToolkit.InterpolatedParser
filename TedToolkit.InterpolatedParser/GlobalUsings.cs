#pragma warning disable RCS1056

#if NET6_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
global using StringPart = System.ReadOnlySpan<char>;
#else
global using StringPart = string;
#endif