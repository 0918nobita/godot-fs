using Godot;

public class Main : Node {
	[Signal]
	public delegate void CustomSignal(int value);

	public override void _Ready()
	{
		EmitSignal(nameof(CustomSignal), 200);
	}
}
