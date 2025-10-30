namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Rect2 JSON converter.</summary>
public class Rect2Converter : JsonConverter<Rect2>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Rect2);

  /// <inheritdoc />
  public override Rect2 Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var position = Vector2.Zero;
    var size = Vector2.Zero;

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Rect2(position, size);
      }

      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName)
      {
        case "position":
          position = JsonSerializer.Deserialize<Vector2>(ref reader, options);
          break;
        case "size":
          size = JsonSerializer.Deserialize<Vector2>(ref reader, options);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Rect2.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Rect2 value,
    JsonSerializerOptions options
  )
  {
    var resolver = options.TypeInfoResolver;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector2), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("position");
    JsonSerializer.Serialize(writer, value.Position, vectorTypeInfo);
    writer.WritePropertyName("size");
    JsonSerializer.Serialize(writer, value.Size, vectorTypeInfo);
    writer.WriteEndObject();
  }
}
