namespace TedToolkit.InterpolatedParser;

/// <summary>
/// The helper for the holders.
/// </summary>
internal static class InterpolatedParserHelper
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
}