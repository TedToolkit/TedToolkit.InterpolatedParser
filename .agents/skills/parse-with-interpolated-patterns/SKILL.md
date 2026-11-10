---
name: parse-with-interpolated-patterns
description: >-
  Extract typed values with TedToolkit.InterpolatedParser. Use for interpolated string parsing, regular-expression patterns, or a custom IInterpolatedParser implementation.
---

# Parse with Interpolated Patterns

- Initialize the destination locals with their required types and write the pattern as an interpolated string, for example `text.Parse($"Age: {age}")`.
- Use `TryParse` when a mismatch is expected input. Use `ParseRegex` or `TryParseRegex` when the delimiters or repetition genuinely require regex semantics.
- Use the documented format and separator options for numeric and collection fields. Test missing delimiters, invalid values, and a representative successful match.
- Use a custom `IInterpolatedParser<T>` only if the package’s built-in types and format options cannot express the input.
