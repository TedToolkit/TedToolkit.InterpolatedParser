using TedToolkit.InterpolatedParser.InterpolatedParsers;

namespace TedToolkit.InterpolatedParser.InterpolatedParserCreators;
#pragma warning disable CA1062

/// <summary>
/// The list interpolated parser creator.
/// </summary>
public sealed class ListInterpolatedParserCreator : IInterpolatedParserCreator
{
    /// <inheritdoc />
    public bool CanCreate(Type type)
    {
        if (!type.IsGenericType)
            return false;

        return type.GetGenericTypeDefinition() == typeof(List<>);
    }

    /// <inheritdoc />
    public IInterpolatedParser Create(Type type)
    {
        var itemType = type.GetGenericArguments()[0];
        var parser = InterpolatedParserSettings.GetParser(itemType);

        return (IInterpolatedParser)
            Activator.CreateInstance(typeof(ListInterpolatedParser<>)
                .MakeGenericType(itemType), parser)!;
    }
}