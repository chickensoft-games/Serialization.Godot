namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Rect2I JSON converter.</summary>
public class Rect2IConverter : JsonConverter<Rect2I>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Rect2I);

  /// <inheritdoc />
  public override Rect2I Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var position = Vector2I.Zero;
    var size = Vector2I.Zero;

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Rect2I(position, size);
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
          position = JsonSerializer.Deserialize<Vector2I>(ref reader, options);
          break;
        case "size":
          size = JsonSerializer.Deserialize<Vector2I>(ref reader, options);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Rect2I.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Rect2I value,
    JsonSerializerOptions options
  )
  {
    var resolver = options.TypeInfoResolver;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector2I), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("position");
    JsonSerializer.Serialize(writer, value.Position, vectorTypeInfo);
    writer.WritePropertyName("size");
    JsonSerializer.Serialize(writer, value.Size, vectorTypeInfo);
    writer.WriteEndObject();
  }
}
