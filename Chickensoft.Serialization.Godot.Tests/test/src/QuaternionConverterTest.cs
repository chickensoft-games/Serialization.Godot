namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class QuaternionConverterTest : TestClass
{
  public QuaternionConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new QuaternionConverter();
    converter.CanConvert(typeof(Quaternion)).ShouldBeTrue();
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

    var obj = new Quaternion(1, 2, 3, 4);
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "x": 1,
        "y": 2,
        "z": 3,
        "w": 4
      }
      """
    );

    var deserialized = JsonSerializer.Deserialize<Quaternion>(json, options);

    deserialized.ShouldBe(obj);
  }
}
