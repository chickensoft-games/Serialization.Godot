namespace Chickensoft.Serialization.Godot.Tests;

using Chickensoft.GoDotTest;
using System.Text.Json;
using global::Godot;
using Shouldly;

public class Rect2IConverterTest : TestClass {
  public Rect2IConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert() {
    var converter = new Rect2IConverter();
    converter.CanConvert(typeof(Rect2I)).ShouldBeTrue();
  }

  [Test]
  public void Converts() {
    GodotSerialization.Setup();

    var options = new JsonSerializerOptions() {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Rect2I(
      new Vector2I(1, 2),
      new Vector2I(3, 4)
    );

    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "position": {
          "x": 1,
          "y": 2
        },
        "size": {
          "x": 3,
          "y": 4
        }
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Rect2I>(json, options);

    deserialized.ShouldBe(obj);
  }
}
