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

public partial class detector : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public Godot.Collections.Array<MeshInstance3D> Lights;

	[Export] 
	public Node3D FlagPlacementLocation;

	[Export] 
	private AudioStreamPlayer3D Beeper;

	public bool Detection = false;

	private float MineDepth = 0.0f;

	private anti_personnel_mine CurrentMine;

	private MineFlag DetectedFlag;

	[Export] 
	public PackedScene MineFlagScene;

	[Export] 
	private player ThePlayer;

	[Signal]
	public delegate void FalseFlagEventHandler();
	
	[Signal]
	public delegate void RemoveFalseFlagEventHandler();
		
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Detected(Area3D Mine)
	{
		if (Mine is MineFlag)
		{
			DetectedFlag = (MineFlag)Mine;
			return;
		}
		
		if (!(Mine is anti_personnel_mine))
		{
			return;
		}
		
		Detection = true;

		MineDepth = ((anti_personnel_mine) Mine).Depth;
		
		GD.Print("Found");

		CurrentMine = ((anti_personnel_mine) Mine);
		
		SetLights();

		Beeper.Playing = true;
		
		ThePlayer.OnDetect();
	}
	
	public void EndDetected(Area3D Mine)
	{
		if (Mine is MineFlag)
		{
			DetectedFlag = null;
			return;
		}
		
		if (!(Mine is anti_personnel_mine))
		{
			return;
		}

		MineDepth = 0.0f;
		Detection = false;
		GD.Print("Unfound");

		CurrentMine = null;
		
		Beeper.Playing = false;
		
		SetLights();
	}

	private void SetLights()
	{
		for (int i = 0; i < Lights.Count; i++)
		{
			if ((i + 1.0) <= MineDepth)
			{
				Lights[i].SetInstanceShaderParameter("Brightness", 1.0f);
			}
			else
			{
				Lights[i].SetInstanceShaderParameter("Brightness", 0.0f);
			}
		}
	}

	public void Flag()
	{
		if (Detection)
		{
			if (CurrentMine.Flagged)
			{
				return;
			}
			
			CurrentMine.Flag(MineFlagScene, GetParent<Node3D>().Rotation);
			GD.Print("Flag");
			ThePlayer.OnFlagMine();
		}
		else
		{
			MineFlag FalseFlag = MineFlagScene.Instantiate<MineFlag>();
			
			GetTree().GetFirstNodeInGroup("LevelRoot").AddChild(FalseFlag);

			FalseFlag.Visible = true;

			FalseFlag.Position = FlagPlacementLocation.GlobalPosition + new Vector3(0.0f, 0.15f, 0.0f);
			FalseFlag.Rotation = GetParent<Node3D>().Rotation;
			GD.Print("FalseFlag");

			EmitSignal("FalseFlag");
		}
		
		ThePlayer.OnFlag();
	}

	public void RemoveFlag()
	{
		if (DetectedFlag != null)
		{
			if (DetectedFlag.MineOwner == null)
			{
				EmitSignal("RemoveFalseFlag");
			}
			else
			{
				DetectedFlag.MineOwner.UnFlag();
			}

			ThePlayer.OnRemoveFlag();
			
			DetectedFlag.QueueFree();
		}
	}
}
