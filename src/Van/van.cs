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

public partial class van : StaticBody3D
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	public bool Enabled = true;
	
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void PlayerNearby(Node3D Player)
	{
		if (!Enabled)
		{
			return;
		}
		
		if (Player is player)
		{
			((player) Player).ToggleCanLeave();
		}
	}
	
	public void PlayerLeft(Node3D Player)
	{
		if (!Enabled)
		{
			return;
		}
		
		if (Player is player)
		{
			((player) Player).ToggleCanLeave();
		}
	}
}
