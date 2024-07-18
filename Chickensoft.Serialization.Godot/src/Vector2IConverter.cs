namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Vector2I JSON converter.</summary>
public class Vector2IConverter : JsonConverter<Vector2I> {
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Vector2I);

  /// <inheritdoc />
  public override Vector2I Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  ) {
    var x = 0;
    var y = 0;

    while (reader.Read()) {
      if (reader.TokenType == JsonTokenType.EndObject) {
        return new Vector2I(x, y);
      }

      if (reader.TokenType != JsonTokenType.PropertyName) {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName) {
        case "x":
          x = reader.GetInt32();
          break;
        case "y":
          y = reader.GetInt32();
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Vector2I.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Vector2I value,
    JsonSerializerOptions options
  ) {
    writer.WriteStartObject();
    writer.WriteNumber("x", value.X);
    writer.WriteNumber("y", value.Y);
    writer.WriteEndObject();
  }
}
