namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Aabb JSON converter.</summary>
public class AabbConverter : JsonConverter<Aabb>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Aabb);

  /// <inheritdoc />
  public override Aabb Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var position = new Vector3();
    var size = new Vector3();

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Aabb(position, size);
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
          position = JsonSerializer.Deserialize<Vector3>(ref reader, options);
          break;
        case "size":
          size = JsonSerializer.Deserialize<Vector3>(ref reader, options);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Aabb.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Aabb value,
    JsonSerializerOptions options
  )
  {
    var resolver = options.TypeInfoResolver;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector3), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("position");
    JsonSerializer.Serialize(writer, value.Position, vectorTypeInfo);
    writer.WritePropertyName("size");
    JsonSerializer.Serialize(writer, value.Size, vectorTypeInfo);
    writer.WriteEndObject();
  }
}
