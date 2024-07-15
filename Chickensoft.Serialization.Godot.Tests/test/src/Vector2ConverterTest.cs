namespace Chickensoft.Serialization.Godot.Tests;

using Chickensoft.GoDotTest;
using System.Text.Json;
using global::Godot;
using Shouldly;

public class Vector2ConverterTest : TestClass {
  public Vector2ConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert() {
    var converter = new Vector2Converter();
    converter.CanConvert(typeof(Vector2)).ShouldBeTrue();
  }

  [Test]
  public void Converts() {
    GodotSerialization.Setup();

    var options = new JsonSerializerOptions() {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Vector2(1, 2);
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "x": 1,
        "y": 2
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Vector2>(json, options);

    deserialized.ShouldBe(obj);
  }
}
