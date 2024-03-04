using Godot;
using System;

public partial class SharedLevelData : Node
{
	public PackedScene LastLevel;

	public PackedScene NextLevel;

	public PackedScene DeathScene;
	public PackedScene ScoreScene;
	public PackedScene FailedScene;

	public int LevelScore = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DeathScene = ResourceLoader.Load<PackedScene>("res://Levels/Death.tscn");
		ScoreScene = ResourceLoader.Load<PackedScene>("res://Levels/Score.tscn");
		FailedScene = ResourceLoader.Load<PackedScene>("res://Levels/Failure.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
