namespace Chickensoft.Serialization.Godot.Tests;

using System.Reflection;
using global::Godot;
using Chickensoft.GoDotTest;

public partial class Tests : Node2D {
  public override void _Ready() => CallDeferred(MethodName.RunTests);

  public void RunTests() =>
    GoTest.RunTests(Assembly.GetExecutingAssembly(), this);
}
