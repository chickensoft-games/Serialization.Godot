namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Basis JSON converter.</summary>
public class BasisConverter : JsonConverter<Basis> {
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Basis);

  /// <inheritdoc />
  public override Basis Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  ) {
    var x = new Vector3();
    var y = new Vector3();
    var z = new Vector3();

    while (reader.Read()) {
      if (reader.TokenType == JsonTokenType.EndObject) {
        return new Basis(x, y, z);
      }

      if (reader.TokenType != JsonTokenType.PropertyName) {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName) {
        case "x":
          x = JsonSerializer.Deserialize<Vector3>(ref reader, options);
          break;
        case "y":
          y = JsonSerializer.Deserialize<Vector3>(ref reader, options);
          break;
        case "z":
          z = JsonSerializer.Deserialize<Vector3>(ref reader, options);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Basis.");

  }

  /// <inheritdoc />
  public override void Write(
    Utf8JsonWriter writer,
    Basis value,
    JsonSerializerOptions options
  ) {
    var resolver = options.TypeInfoResolver;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector3), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("x");
    JsonSerializer.Serialize(writer, value.X, vectorTypeInfo);
    writer.WritePropertyName("y");
    JsonSerializer.Serialize(writer, value.Y, vectorTypeInfo);
    writer.WritePropertyName("z");
    JsonSerializer.Serialize(writer, value.Z, vectorTypeInfo);
    writer.WriteEndObject();
  }
}
