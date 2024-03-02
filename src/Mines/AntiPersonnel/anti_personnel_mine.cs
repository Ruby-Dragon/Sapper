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

	public bool Flagged = false;

	private MeshInstance3D MineFlag;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Flag(MeshInstance3D FlagMesh, Vector3 FlagRotation)
	{
		MineFlag = new MeshInstance3D();
		MineFlag.Mesh = FlagMesh.Mesh;
		AddChild(MineFlag);

		MineFlag.Position = FlagLocation.Position + new Vector3(0.0f, 0.345f, 0.0f);
		MineFlag.Rotation = FlagRotation;
		MineFlag.Visible = true;
		Flagged = true;

		EmitSignal("BeenFlagged");
	}

	public void Explode(Node3D Overlapper)
	{
		if (Overlapper is player)
		{
			((player) Overlapper).Die();
		}
	}
}
