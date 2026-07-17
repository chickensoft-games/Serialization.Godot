namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Vector4I JSON converter.</summary>
public class Vector4IConverter : JsonConverter<Vector4I>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Vector4I);

  /// <inheritdoc />
  public override Vector4I Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var x = 0;
    var y = 0;
    var z = 0;
    var w = 0;

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Vector4I(x, y, z, w);
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
          x = reader.GetInt32();
          break;
        case "y":
          y = reader.GetInt32();
          break;
        case "z":
          z = reader.GetInt32();
          break;
        case "w":
          w = reader.GetInt32();
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Vector4I.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Vector4I value,
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
