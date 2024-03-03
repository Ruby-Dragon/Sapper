using Godot;
using System;

public partial class van : StaticBody3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayerNearby(Node3D Player)
	{
		if (Player is player)
		{
			((player) Player).ToggleCanLeave();
		}
	}
	
	public void PlayerLeft(Node3D Player)
	{
		if (Player is player)
		{
			((player) Player).ToggleCanLeave();
		}
	}
}
