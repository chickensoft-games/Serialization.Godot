namespace Chickensoft.Serialization.Godot;

using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Color JSON converter.</summary>
public class ColorConverter : JsonConverter<Color> {
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Color);

  /// <inheritdoc />
  public override Color Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  ) {
    var rgba = (uint)0x000000ff;

    while (reader.Read()) {
      if (reader.TokenType == JsonTokenType.EndObject) {
        return new Color(rgba);
      }

      if (reader.TokenType != JsonTokenType.PropertyName) {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName) {
        case "rgba":
          rgba = uint.Parse(reader.GetString() ?? string.Empty, NumberStyles.HexNumber);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Color.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Color value,
    JsonSerializerOptions options
  ) {
    writer.WriteStartObject();
    writer.WriteString("rgba", value.ToRgba32().ToString("X"));
    writer.WriteEndObject();
  }
}
