namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using global::Godot;

/// <summary>Color JSON converter.</summary>
public partial class ColorConverter : JsonConverter<Color> {
  [GeneratedRegex("\\s*rgba\\((.*)\\)\\s*", RegexOptions.IgnoreCase, "en-US")]
  private static partial Regex RgbaRegex();

  [GeneratedRegex("\\s+", RegexOptions.IgnoreCase, "en-US")]
  private static partial Regex WhitespaceRegex();

  private static readonly Regex _rgbaRegex = RgbaRegex();
  private static readonly Regex _whitespaceRegex = WhitespaceRegex();

  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Color);

  /// <inheritdoc />
  public override Color Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  ) {
    var r = 0f;
    var g = 0f;
    var b = 0f;
    var a = 1f;

    while (reader.Read()) {
      if (reader.TokenType == JsonTokenType.EndObject) {
        return new Color(r, g, b, a);
      }

      if (reader.TokenType != JsonTokenType.PropertyName) {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName) {
        case "rgba":
          var match = _rgbaRegex.Match(reader.GetString() ?? string.Empty);
          if (match.Groups[1].Success) {
            var values = _whitespaceRegex
              .Replace(match.Groups[1].Value, "")
              .Split(",");

            if (values.Length == 4) {
              r = float.Parse(values[0]);
              g = float.Parse(values[1]);
              b = float.Parse(values[2]);
              a = float.Parse(values[3]);
            }
            else {
              throw new JsonException("Unable to parse property 'rgba' of Color");
            }
          }
          else {
            throw new JsonException("Unable to parse property 'rgba' of Color");
          }

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
    writer.WriteString("rgba", $"rgba({value.R}, {value.G}, {value.B}, {value.A})");
    writer.WriteEndObject();
  }
}
