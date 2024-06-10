namespace Chickensoft.Serialization.Godot.Tests;

using Chickensoft.GoDotTest;
using System.Text.Json;
using global::Godot;
using Shouldly;

public class BasisConverterTest : TestClass {
  public BasisConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert() {
    var converter = new BasisConverter();
    converter.CanConvert(typeof(Basis)).ShouldBeTrue();
  }

  [Test]
  public void Converts() {
    var options = new JsonSerializerOptions() {
      WriteIndented = true,
      TypeInfoResolver = new SerializableTypeResolver(),
    };

    var obj = new Basis(
      new Vector3(1, 2, 3),
      new Vector3(4, 5, 6),
      new Vector3(7, 8, 9)
    );
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "x": {
          "x": 1,
          "y": 2,
          "z": 3
        },
        "y": {
          "x": 4,
          "y": 5,
          "z": 6
        },
        "z": {
          "x": 7,
          "y": 8,
          "z": 9
        }
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Basis>(json, options);

    deserialized.ShouldBe(obj);
  }
}
