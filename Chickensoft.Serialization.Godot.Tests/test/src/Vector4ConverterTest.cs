namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class Vector4ConverterTest : TestClass
{
  public Vector4ConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new Vector4Converter();
    converter.CanConvert(typeof(Vector4)).ShouldBeTrue();
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

    var obj = new Vector4(1, 2, 3, 4);
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "x": 1,
        "y": 2,
        "z": 3,
        "w": 4
      }
      """
      , StringCompareShould.IgnoreLineEndings);

    var deserialized = JsonSerializer.Deserialize<Vector4>(json, options);

    deserialized.ShouldBe(obj);
  }
}
