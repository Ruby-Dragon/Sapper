using Godot;
using System;

public partial class Score : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public Label Refrain;
	
	public override void _Ready()
	{
		//int Score = GetNode<SharedLevelData>("/root/SharedLevelData").LevelScore;
		int Score = 90;
		if (Score < 100)
		{
			Refrain.Visible = true;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (Input.IsActionJustPressed("Interact"))
		{
			GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").NextLevel);
		}
	}
}
