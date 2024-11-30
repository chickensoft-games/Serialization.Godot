namespace Chickensoft.Serialization.Godot.Tests;

using Chickensoft.GoDotTest;
using System.Text.Json;
using global::Godot;
using Shouldly;

public class ColorConverterTest : TestClass {
  public ColorConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert() {
    var converter = new ColorConverter();
    converter.CanConvert(typeof(Color)).ShouldBeTrue();
  }

  [Test]
  public void Converts() {
    GodotSerialization.Setup();

    var options = new JsonSerializerOptions() {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Color(0xff0000ff);
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "rgba": "FF0000FF"
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Color>(json, options);

    deserialized.ShouldBe(obj);
  }
}
