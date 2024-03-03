using Godot;
using System;

public partial class Level : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int MinesToFlag;

	public int FalseFlags = 0;

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
			
		}
	}

	public void GoToNextLevel()
	{
		GetTree().ChangeSceneToPacked(NextLevel);
	}

	public void UpdateFalseFlag()
	{
		FalseFlags += 1;
	}
}
