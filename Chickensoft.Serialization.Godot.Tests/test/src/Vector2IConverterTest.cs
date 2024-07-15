namespace Chickensoft.Serialization.Godot.Tests;

using System;
using Chickensoft.GoDotTest;
using System.Text.Json;
using global::Godot;
using Shouldly;

public class Vector2IConverterTest : TestClass {
  public Vector2IConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert() {
    var converter = new Vector2IConverter();
    converter.CanConvert(typeof(Vector2I)).ShouldBeTrue();
  }

  [Test]
  public void Converts() {
    GodotSerialization.Setup();

    var options = new JsonSerializerOptions() {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Vector2I(1, 2);
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

    var deserialized = JsonSerializer.Deserialize<Vector2I>(json, options);

    deserialized.ShouldBe(obj);
  }

  [Test]
  public void ThrowsOnDecimals() {
    GodotSerialization.Setup();

    const string json = """{"x": 1.2, "y": 2.3}""";
    var options = new JsonSerializerOptions() {
      TypeInfoResolver = new SerializableTypeResolver(),
    };
    Should.Throw<JsonException>(() => JsonSerializer.Deserialize<Vector2I>(json, options));
  }
}
