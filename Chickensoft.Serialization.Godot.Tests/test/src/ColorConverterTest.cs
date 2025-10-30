namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class ColorConverterTest : TestClass
{
  public ColorConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new ColorConverter();
    converter.CanConvert(typeof(Color)).ShouldBeTrue();
  }

  [Test]
  public void Converts()
  {
    GodotSerialization.Setup();

    var options = new JsonSerializerOptions()
    {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Color(1f, 0.5f, 0f, 1f);
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "rgba": "rgba(1, 0.5, 0, 1)"
      }
      """
    , StringCompareShould.IgnoreLineEndings);

    var deserialized = JsonSerializer.Deserialize<Color>(json, options);

    deserialized.ShouldBe(obj);
  }
}
