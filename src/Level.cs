using Godot;
using System;

public partial class Level : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int MinesToFlag = 0;

	private float TotalMines;

	public float FalseFlags = 0;

	[Export] 
	public PackedScene NextLevel;
	public override void _Ready()
	{
		TotalMines = MinesToFlag;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateScore()
	{
		MinesToFlag -= 1;
	}

	public void GoToNextLevel()
	{
		GD.Print(CreateScore());
		GetTree().ChangeSceneToPacked(NextLevel);
	}

	private int CreateScore()
	{
		if (MinesToFlag > 0)
		{
			return 0;
		}

		return (int) ((FalseFlags / TotalMines) * 100.0f);
	}

	public void UpdateFalseFlag()
	{
		FalseFlags += 1;
	}
}
