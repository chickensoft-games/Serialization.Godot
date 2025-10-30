namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class Vector3IConverterTest : TestClass
{
  public Vector3IConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new Vector3IConverter();
    converter.CanConvert(typeof(Vector3I)).ShouldBeTrue();
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

    var obj = new Vector3I(1, 2, 3);
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "x": 1,
        "y": 2,
        "z": 3
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Vector3I>(json, options);

    deserialized.ShouldBe(obj);
  }

  [Test]
  public void ThrowsOnDecimals()
  {
    GodotSerialization.Setup();

    const string json = """{"x": 1, "y": 2.3, "z": 3}""";
    var options = new JsonSerializerOptions()
    {
      TypeInfoResolver = new SerializableTypeResolver(),
    };
    Should.Throw<JsonException>(() => JsonSerializer.Deserialize<Vector3I>(json, options));
  }
}
