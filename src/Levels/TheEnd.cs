using Godot;
using System;

public partial class TheEnd : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Confined;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Quit()
	{
		GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").MainMenuScene);
	}
}
