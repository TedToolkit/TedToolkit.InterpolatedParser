using System.Globalization;

namespace TedToolkit.InterpolatedParser.InterpolatedParsers;
#pragma warning disable CA1062

/// <summary>
/// The helper for the holders.
/// </summary>
public static class InterpolatedParserHelper
{
    /// <summary>
    /// Get the format items.
    /// </summary>
    /// <param name="format">format.</param>
    /// <returns>result.</returns>
    public static string[] GetFormatItems(string format)
        => format.Split('|');

    /// <summary>
    /// Get the string.
    /// </summary>
    /// <param name="items">items.</param>
    /// <param name="index">index.</param>
    /// <param name="defaultValue">default value.</param>
    /// <returns>result.</returns>
    public static string GetString(string[] items, int index, string defaultValue)
    {
        if (items.Length <= index)
            return defaultValue;

        var text = items[index];
        if (string.IsNullOrEmpty(text))
            return defaultValue;

        return text;
    }

    /// <summary>
    /// Get the number style.
    /// </summary>
    /// <param name="format">format.</param>
    /// <returns>style.</returns>
    public static NumberStyles GetNumberStyle(string format)
    {
        if (string.IsNullOrEmpty(format))
            return NumberStyles.Any;

        if (Enum.TryParse<NumberStyles>(format, out var value))
            return value;

        return NumberStyles.Any;
    }
}