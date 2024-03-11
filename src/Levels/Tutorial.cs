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
using Godot.Collections;

public partial class Tutorial : Node3D
{
	// Called when the node enters the scene tree for the first time.
	private Godot.Collections.Array<String> AllTaskMessages = new Array<string>();

	private int CurrentTask = 0;

	[Export] 
	private player ThePlayer;

	[Export] 
	private van Door;
	
	public override void _Ready()
	{
		AllTaskMessages.Add("Detect the mine");
		AllTaskMessages.Add("Place a flag with F");
		AllTaskMessages.Add("Remove a flag with R");
		AllTaskMessages.Add("Flag the mine");
		AllTaskMessages.Add("Go through the door to deploy");
		
		ThePlayer.ChangeMissionText(AllTaskMessages[0]);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void NextTask()
	{
		CurrentTask += 1;
		
		ThePlayer.ChangeMissionText(AllTaskMessages[CurrentTask]);

		if (CurrentTask == 4)
		{
			Door.Enabled = true;
		}
	}

	public void Detected()
	{
		if (CurrentTask == 0)
		{
			NextTask();
		}
	}

	public void FlagPlaced()
	{
		if (CurrentTask == 1)
		{
			NextTask();
		}
	}

	public void RemoveFlag()
	{
		if (CurrentTask == 2)
		{
			NextTask();
		}
	}

	public void FlagMine()
	{
		if (CurrentTask == 3)
		{
			NextTask();
		}
	}
}
