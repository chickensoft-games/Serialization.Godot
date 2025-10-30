namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class Transform3DConverterTest : TestClass
{
  public Transform3DConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new Transform3DConverter();
    converter.CanConvert(typeof(Transform3D)).ShouldBeTrue();
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

    var obj = new Transform3D(
      new Basis(
        new Vector3(1, 2, 3),
        new Vector3(4, 5, 6),
        new Vector3(7, 8, 9)
      ),
      new Vector3(10, 11, 12)
    );

    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "basis": {
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
        },
        "origin": {
          "x": 10,
          "y": 11,
          "z": 12
        }
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Transform3D>(json, options);

    deserialized.ShouldBe(obj);
  }
}
