namespace Chickensoft.Serialization.Godot;

using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using global::Godot;

/// <summary>Transform3D JSON converter.</summary>
public class Transform3DConverter : JsonConverter<Transform3D> {
  /// <inheritdoc />
  public override bool CanConvert(Type typeToConvert) =>
    typeToConvert == typeof(Transform3D);

  /// <inheritdoc />
  public override Transform3D Read(
    ref Utf8JsonReader reader,
    Type typeToConvert,
    JsonSerializerOptions options
  ) {
    var basis = new Basis();
    var origin = new Vector3();

    while (reader.Read()) {
      if (reader.TokenType == JsonTokenType.EndObject) {
        return new Transform3D(basis, origin);
      }

      if (reader.TokenType != JsonTokenType.PropertyName) {
        continue;
      }

      var propertyName = reader.GetString();
      reader.Read();

      switch (propertyName) {
        case "basis":
          basis = JsonSerializer.Deserialize<Basis>(ref reader, options);
          break;
        case "origin":
          origin = JsonSerializer.Deserialize<Vector3>(ref reader, options);
          break;
        default:
          break;
      }
    }

    throw new JsonException("Unexpected end when reading Transform3D.");
  }

  /// <inheritdoc />
  public override void Write(Utf8JsonWriter writer, Transform3D value, JsonSerializerOptions options) {
    var resolver = options.TypeInfoResolver;
    var basisTypeInfo = resolver!.GetTypeInfo(typeof(Basis), options)!;
    var vectorTypeInfo = resolver!.GetTypeInfo(typeof(Vector3), options)!;

    writer.WriteStartObject();
    writer.WritePropertyName("basis");
    JsonSerializer.Serialize(writer, value.Basis, basisTypeInfo);
    writer.WritePropertyName("origin");
    JsonSerializer.Serialize(writer, value.Origin, vectorTypeInfo);
    writer.WriteEndObject();
  }
}
