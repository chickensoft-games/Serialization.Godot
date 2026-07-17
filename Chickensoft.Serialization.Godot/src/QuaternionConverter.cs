namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Quaternion JSON converter.</summary>
public class QuaternionConverter : JsonConverter<Quaternion>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Quaternion);

  /// <inheritdoc />
  public override Quaternion Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var x = 0f;
    var y = 0f;
    var z = 0f;
    var w = 0f;

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Quaternion(x, y, z, w);
      }

      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName)
      {
        case "x":
          x = reader.GetSingle();
          break;
        case "y":
          y = reader.GetSingle();
          break;
        case "z":
          z = reader.GetSingle();
          break;
        case "w":
          w = reader.GetSingle();
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Quaternion.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Quaternion value,
    JsonSerializerOptions options
  )
  {
    writer.WriteStartObject();
    writer.WriteNumber("x", value.X);
    writer.WriteNumber("y", value.Y);
    writer.WriteNumber("z", value.Z);
    writer.WriteNumber("w", value.W);
    writer.WriteEndObject();
  }
}
