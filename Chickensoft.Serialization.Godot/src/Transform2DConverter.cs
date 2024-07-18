namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Transform2D JSON converter.</summary>
public class Transform2DConverter : JsonConverter<Transform2D> {
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Transform2D);

  /// <inheritdoc />
  public override Transform2D Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  ) {
    var x = new Vector2();
    var y = new Vector2();
    var origin = new Vector2();

    while (reader.Read()) {
      if (reader.TokenType == JsonTokenType.EndObject) {
        return new Transform2D(x, y, origin);
      }

      if (reader.TokenType != JsonTokenType.PropertyName) {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName) {
        case "x":
          x = JsonSerializer.Deserialize<Vector2>(ref reader, options);
          break;
        case "y":
          y = JsonSerializer.Deserialize<Vector2>(ref reader, options);
          break;
        case "origin":
          origin = JsonSerializer.Deserialize<Vector2>(ref reader, options);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Transform2D.");
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, Transform2D value, JsonSerializerOptions options) {
    var resolver = options.TypeInfoResolver;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector2), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("x");
    JsonSerializer.Serialize(writer, value.X, vectorTypeInfo);
    writer.WritePropertyName("y");
    JsonSerializer.Serialize(writer, value.Y, vectorTypeInfo);
    writer.WritePropertyName("origin");
    JsonSerializer.Serialize(writer, value.Origin, vectorTypeInfo);
    writer.WriteEndObject();
  }
}
