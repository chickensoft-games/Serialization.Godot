namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Plane JSON converter.</summary>
public class PlaneConverter : JsonConverter<Plane>
{
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Plane);

  /// <inheritdoc />
  public override Plane Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  )
  {
    var normal = new Vector3();
    var d = 0f;

    while (reader.Read())
    {
      if (reader.TokenType == JsonTokenType.EndObject)
      {
        return new Plane(normal, d);
      }

      if (reader.TokenType != JsonTokenType.PropertyName)
      {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName)
      {
        case "normal":
          normal = JsonSerializer.Deserialize<Vector3>(ref reader, options);
          break;
        case "d":
          d = reader.GetSingle();
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Plane.");
  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Plane value,
    JsonSerializerOptions options
  )
  {
    var resolver = options.TypeInfoResolver;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector3), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("normal");
    JsonSerializer.Serialize(writer, value.Normal, vectorTypeInfo);
    writer.WriteNumber("d", value.D);
    writer.WriteEndObject();
  }
}
