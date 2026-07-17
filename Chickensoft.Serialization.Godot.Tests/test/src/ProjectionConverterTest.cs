namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class ProjectionConverterTest : TestClass
{
  public ProjectionConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new ProjectionConverter();
    converter.CanConvert(typeof(Projection)).ShouldBeTrue();
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

    var obj = new Projection(
      new Vector4(1, 2, 3, 4),
      new Vector4(5, 6, 7, 8),
      new Vector4(9, 10, 11, 12),
      new Vector4(13, 14, 15, 16)
    );
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "x": {
          "x": 1,
          "y": 2,
          "z": 3,
          "w": 4
        },
        "y": {
          "x": 5,
          "y": 6,
          "z": 7,
          "w": 8
        },
        "z": {
          "x": 9,
          "y": 10,
          "z": 11,
          "w": 12
        },
        "w": {
          "x": 13,
          "y": 14,
          "z": 15,
          "w": 16
        }
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Projection>(json, options);

    deserialized.ShouldBe(obj);
  }
}
