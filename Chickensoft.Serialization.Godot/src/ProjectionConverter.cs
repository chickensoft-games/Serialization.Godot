namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Projection JSON converter.</summary>
public class ProjectionConverter : JsonConverter<Projection>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Projection);

  /// <inheritdoc />
  public override Projection Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var x = new Vector4();
    var y = new Vector4();
    var z = new Vector4();
    var w = new Vector4();

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Projection(x, y, z, w);
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
          x = JsonSerializer.Deserialize<Vector4>(ref reader, options);
          break;
        case "y":
          y = JsonSerializer.Deserialize<Vector4>(ref reader, options);
          break;
        case "z":
          z = JsonSerializer.Deserialize<Vector4>(ref reader, options);
          break;
        case "w":
          w = JsonSerializer.Deserialize<Vector4>(ref reader, options);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Projection.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Projection value,
    JsonSerializerOptions options
  )
  {
    var resolver = options.TypeInfoResolver;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector4), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("x");
    JsonSerializer.Serialize(writer, value.X, vectorTypeInfo);
    writer.WritePropertyName("y");
    JsonSerializer.Serialize(writer, value.Y, vectorTypeInfo);
    writer.WritePropertyName("z");
    JsonSerializer.Serialize(writer, value.Z, vectorTypeInfo);
    writer.WritePropertyName("w");
    JsonSerializer.Serialize(writer, value.W, vectorTypeInfo);
    writer.WriteEndObject();
  }
}
