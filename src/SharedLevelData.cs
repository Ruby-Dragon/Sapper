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

public partial class SharedLevelData : Node
{
	public PackedScene LastLevel;

	public PackedScene NextLevel;

	public PackedScene DeathScene;
	public PackedScene ScoreScene;
	public PackedScene FailedScene;
	public PackedScene MainMenuScene;

	public bool SDFGIEnable = true;
	public bool SSILEnable = true;
	public bool SSAOEnable = true;

	public int LevelScore = 0;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		DeathScene = ResourceLoader.Load<PackedScene>("res://Levels/Death.tscn");
		ScoreScene = ResourceLoader.Load<PackedScene>("res://Levels/Score.tscn");
		FailedScene = ResourceLoader.Load<PackedScene>("res://Levels/Failure.tscn");
		MainMenuScene = ResourceLoader.Load<PackedScene>("res://Levels/MainMenu.tscn");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
