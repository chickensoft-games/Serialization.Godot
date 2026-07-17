namespace Chickensoft.Serialization.Godot.Tests;

using System.Text.Json;
using Chickensoft.GoDotTest;
using global::Godot;
using Shouldly;

public class PlaneConverterTest : TestClass
{
  public PlaneConverterTest(Node testScene) : base(testScene) { }

  [Test]
  public void CanConvert()
  {
    var converter = new PlaneConverter();
    converter.CanConvert(typeof(Plane)).ShouldBeTrue();
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

    var obj = new Plane(
      new Vector3(1, 2, 3),
      7
    );
    var json = JsonSerializer.Serialize(obj, options);

    json.ShouldBe(
      /*lang=json*/
      """
      {
        "normal": {
          "x": 1,
          "y": 2,
          "z": 3
        },
        "d": 7
      }
      """
      , StringCompareShould.IgnoreLineEndings);

    var deserialized = JsonSerializer.Deserialize<Plane>(json, options);

    deserialized.ShouldBe(obj);
  }
}
