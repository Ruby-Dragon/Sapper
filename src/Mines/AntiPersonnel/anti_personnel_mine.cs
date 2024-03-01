using Godot;
using System;

public partial class anti_personnel_mine : Area3D
{
	[Export]
	public float Depth;

	public bool Flagged = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Flag()
	{
		Flagged = true;
	}
}
