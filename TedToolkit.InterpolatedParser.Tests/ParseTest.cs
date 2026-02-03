using TedToolkit.InterpolatedParser.Tests.Data;

namespace TedToolkit.InterpolatedParser.Tests;

public class ParseTest
{
    [Test]
    public async Task Should_get_value_at_middle()
    {
        var name = "";
        "I am Ted!".Parse($"I am {name}!");
        await Assert.That(name).IsEqualTo("Ted");
    }

    [Test]
    public async Task Should_get_value_at_the_end()
    {
        var name = "";
        "I am Ted".Parse($"I am {name}");
        await Assert.That(name).IsEqualTo("Ted");
    }

    [Test]
    public async Task Should_get_value_at_the_start()
    {
        var name = "";
        "Ted is me!".Parse($"{name} is me!");
        await Assert.That(name).IsEqualTo("Ted");
    }

    [Test]
    public async Task Should_get_three_items()
    {
        string[] names = [];
        "There are A,B,C".Parse($"There are {names}");
        await Assert.That(names.Length).IsEqualTo(3);
        await Assert.That(names[0]).IsEqualTo("A");
        await Assert.That(names[1]).IsEqualTo("B");
        await Assert.That(names[2]).IsEqualTo("C");
    }

    [Test]
    public async Task Should_get_three_items_by_custom_separator()
    {
        string[] names = [];
        "There are A;B;C.".Parse($"There are {names:;}.");
        await Assert.That(names.Length).IsEqualTo(3);
        await Assert.That(names[0]).IsEqualTo("A");
        await Assert.That(names[1]).IsEqualTo("B");
        await Assert.That(names[2]).IsEqualTo("C");
    }

    [Test]
    public async Task Should_get_three_enum_items_by_custom_separator()
    {
        ExampleEnum[] names = [];
        "There are A;B;C.".Parse($"There are {names:;}.");
        await Assert.That(names.Length).IsEqualTo(3);
        await Assert.That(names[0]).IsEqualTo(ExampleEnum.A);
        await Assert.That(names[1]).IsEqualTo(ExampleEnum.B);
        await Assert.That(names[2]).IsEqualTo(ExampleEnum.C);
    }
}