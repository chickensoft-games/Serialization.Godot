namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class Vector3ConverterTest : TestClass
{
  public Vector3ConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new Vector3Converter();
    converter.CanConvert(typeof(Vector3)).ShouldBeTrue();
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

    var obj = new Vector3(1, 2, 3);
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

    var deserialized = JsonSerializer.Deserialize<Vector3>(json, options);

    deserialized.ShouldBe(obj);
  }
}
