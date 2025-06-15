namespace Chickensoft.Serialization.Godot.Tests;

using Chickensoft.GoDotTest;
using System.Text.Json;
using global::Godot;
using Shouldly;

public class Rect2ConverterTest : TestClass {
  public Rect2ConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert() {
    var converter = new Rect2Converter();
    converter.CanConvert(typeof(Rect2)).ShouldBeTrue();
  }

  [Test]
  public void Converts() {
    GodotSerialization.Setup();

    var options = new JsonSerializerOptions() {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Rect2(
      new Vector2(1, 2),
      new Vector2(3, 4)
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

    var deserialized = JsonSerializer.Deserialize<Rect2>(json, options);

    deserialized.ShouldBe(obj);
  }
}
