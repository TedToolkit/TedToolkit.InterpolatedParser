namespace TedToolkit.InterpolatedParser.InterpolatedParserCreators;
#pragma warning disable CA1062

/// <summary>
/// The list interpolated parser creator.
/// </summary>
internal sealed class ArrayInterpolatedParserCreator : IInterpolatedParserCreator
{
    /// <inheritdoc />
    public bool CanCreate(Type type)
        => type.IsArray;

    /// <inheritdoc />
    public IInterpolatedParser Create(Type type)
    {
        var itemType = type.GetElementType()!;
        var parser = InterpolatedParserSettings.GetParser(itemType);

        return (IInterpolatedParser)
            Activator.CreateInstance(typeof(ArrayInterpolatedParser<>)
                .MakeGenericType(itemType), parser)!;
    }
}