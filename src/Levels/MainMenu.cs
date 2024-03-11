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

public partial class MainMenu : Node2D
{
	// Called when the node enters the scene tree for the first time.
	[Export] 
	private PackedScene FirstLevel;
	public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Confined;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Quit()
	{
		GetTree().Quit();
	}

	public void Start()
	{
		GetTree().ChangeSceneToPacked(FirstLevel);
	}
}
