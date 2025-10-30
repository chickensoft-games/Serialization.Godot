namespace Chickensoft.Serialization.Godot;

using Chickensoft.Serialization;

/// <summary>
/// Godot serialization.
/// </summary>
public static class GodotSerialization
{
  /// <summary>
  /// Register the converters for Godot types with the Chickensoft Serialization
  /// system.
  /// </summary>
  public static void Setup()
  {
    Serializer.AddConverter(new Vector2Converter());
    Serializer.AddConverter(new Vector2IConverter());
    Serializer.AddConverter(new Transform2DConverter());
    Serializer.AddConverter(new Vector3Converter());
    Serializer.AddConverter(new Vector3IConverter());
    Serializer.AddConverter(new BasisConverter());
    Serializer.AddConverter(new Transform3DConverter());
    Serializer.AddConverter(new ColorConverter());
    Serializer.AddConverter(new Rect2Converter());
    Serializer.AddConverter(new Rect2IConverter());
  }
}
