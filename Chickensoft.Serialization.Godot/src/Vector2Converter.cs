namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Vector2 JSON converter.</summary>
public class Vector2Converter : JsonConverter<Vector2>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Vector2);

  /// <inheritdoc />
  public override Vector2 Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var x = 0f;
    var y = 0f;

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Vector2(x, y);
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
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Vector2.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Vector2 value,
    JsonSerializerOptions options
  )
  {
    writer.WriteStartObject();
    writer.WriteNumber("x", value.X);
    writer.WriteNumber("y", value.Y);
    writer.WriteEndObject();
  }
}
