using Godot;
using System;

public partial class MineFlag : Area3D
{
	// Called when the node enters the scene tree for the first time.
	public anti_personnel_mine MineOwner;
    
    public override void _Ready()
    {
	    MineOwner = null;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
