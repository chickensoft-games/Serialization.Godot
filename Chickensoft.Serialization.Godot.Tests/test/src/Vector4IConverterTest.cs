namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class Vector4IConverterTest : TestClass
{
  public Vector4IConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new Vector4IConverter();
    converter.CanConvert(typeof(Vector4I)).ShouldBeTrue();
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

    var obj = new Vector4I(1, 2, 3, 4);
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
    );

    var deserialized = JsonSerializer.Deserialize<Vector4I>(json, options);

    deserialized.ShouldBe(obj);
  }

  [Test]
  public void ThrowsOnDecimals()
  {
    GodotSerialization.Setup();

    const string json = """{"x": 1.1, "y": 2.2, "z": 3.3, "w": 4.4}""";
    var options = new JsonSerializerOptions()
    {
      TypeInfoResolver = new SerializableTypeResolver(),
    };
    Should.Throw<JsonException>(() => JsonSerializer.Deserialize<Vector4I>(json, options));
  }
}
