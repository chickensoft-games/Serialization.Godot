namespace Chickensoft.Serialization.Godot;

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Chickensoft.Serialization;

/// <summary>
/// Godot serialization.
/// </summary>
public static class GodotSerialization {
#pragma warning disable CA2255
  [ModuleInitializer]
  [ExcludeFromCodeCoverage]
#pragma warning restore CA2255
  internal static void Setup() {
    Serializer.AddConverter(new Vector3Converter());
    Serializer.AddConverter(new BasisConverter());
    Serializer.AddConverter(new Transform3DConverter());
  }
}
