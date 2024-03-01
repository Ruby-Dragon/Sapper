using Godot;
using System;

public partial class Level : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int MinesToFlag;

	[Export] 
	public PackedScene NextLevel;
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateScore()
	{
		MinesToFlag -= 1;
		if (MinesToFlag <= 0)
		{
			GoToNextLevel();
		}
	}

	public void GoToNextLevel()
	{
		GetTree().ChangeSceneToPacked(NextLevel);
	}
}
