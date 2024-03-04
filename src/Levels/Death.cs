using Godot;
using System;

public partial class Death : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void EndDeathScene()
	{
		GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").LastLevel);
	}
}
