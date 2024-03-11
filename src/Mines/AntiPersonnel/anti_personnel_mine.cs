/*
Copyright (C) 2024 Ruby-Dragon
This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

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
