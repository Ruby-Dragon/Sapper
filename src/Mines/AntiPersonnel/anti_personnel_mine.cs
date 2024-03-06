using Godot;
using System;

public partial class anti_personnel_mine : Area3D
{
	[Export]
	public float Depth;

	[Export] 
	public Node3D FlagLocation;

	[Signal]
	public delegate void BeenFlaggedEventHandler();
	
	[Signal]
	public delegate void UnFlaggedEventHandler();

	public bool Flagged = false;

	private MineFlag MineFlagInstance;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Flag(PackedScene MineFlagScene, Vector3 FlagRotation)
	{
		MineFlagInstance = MineFlagScene.Instantiate<MineFlag>();
		AddChild(MineFlagInstance);

		MineFlagInstance.Position = FlagLocation.Position + new Vector3(0.0f, 0.345f, 0.0f);
		MineFlagInstance.Rotation = FlagRotation;
		MineFlagInstance.Visible = true;
		MineFlagInstance.MineOwner = this;
		Flagged = true;

		EmitSignal("BeenFlagged");
	}

	public void UnFlag()
	{
		Flagged = false;
		EmitSignal("UnFlagged");
	}

	public void Explode(Node3D Overlapper)
	{
		if (Overlapper is player)
		{
			((player) Overlapper).Die();
		}
	}
}
