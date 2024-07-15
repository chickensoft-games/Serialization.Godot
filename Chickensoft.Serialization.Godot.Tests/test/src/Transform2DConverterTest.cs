namespace Chickensoft.Serialization.Godot.Tests;

using Chickensoft.GoDotTest;
using System.Text.Json;
using global::Godot;
using Shouldly;

public class Transform2DConverterTest : TestClass {
  public Transform2DConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert() {
    var converter = new Transform2DConverter();
    converter.CanConvert(typeof(Transform2D)).ShouldBeTrue();
  }

  [Test]
  public void Converts() {
    GodotSerialization.Setup();

    var options = new JsonSerializerOptions() {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Transform2D(
      new Vector2(1, 2),
      new Vector2(3, 4),
      new Vector2(5, 6)
    );

    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "x": {
          "x": 1,
          "y": 2
        },
        "y": {
          "x": 3,
          "y": 4
        },
        "origin": {
          "x": 5,
          "y": 6
        }
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Transform2D>(json, options);

    deserialized.ShouldBe(obj);
  }
}
