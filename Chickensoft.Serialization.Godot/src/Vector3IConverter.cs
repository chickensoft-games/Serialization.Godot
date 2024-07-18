namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Vector3I JSON converter.</summary>
public class Vector3IConverter : JsonConverter<Vector3I> {
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Vector3I);

  /// <inheritdoc />
  public override Vector3I Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  ) {
    var x = 0;
    var y = 0;
    var z = 0;

    while (reader.Read()) {
      if (reader.TokenType == JsonTokenType.EndObject) {
        return new Vector3I(x, y, z);
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
        case "z":
          z = reader.GetInt32();
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Vector3I.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Vector3I value,
    JsonSerializerOptions options
  ) {
    writer.WriteStartObject();
    writer.WriteNumber("x", value.X);
    writer.WriteNumber("y", value.Y);
    writer.WriteNumber("z", value.Z);
    writer.WriteEndObject();
  }
}
