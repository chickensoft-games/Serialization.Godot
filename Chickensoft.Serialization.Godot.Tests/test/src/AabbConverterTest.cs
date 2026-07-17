namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class AabbConverterTest : TestClass
{
  public AabbConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new AabbConverter();
    converter.CanConvert(typeof(Aabb)).ShouldBeTrue();
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

    var obj = new Aabb(
      new Vector3(1, 2, 3),
      new Vector3(4, 5, 6)
    );
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "position": {
          "x": 1,
          "y": 2,
          "z": 3
        },
        "size": {
          "x": 4,
          "y": 5,
          "z": 6
        }
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Aabb>(json, options);

    deserialized.ShouldBe(obj);
  }
}
