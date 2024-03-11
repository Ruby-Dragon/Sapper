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

public partial class Level : Node3D
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	public int MinesToFlag = 0;

	[Export]
	public String MissionText;

	[Export] 
	public player ThePlayer;

	private float TotalMines;

	public float FalseFlags = 0;

	[Export] 
	public PackedScene NextLevel;

	[Export] 
	private bool CanUseSDFGI = false;

	[Export] 
	public WorldEnvironment TheWorld;
	public override void _Ready()
	{
		GetNode<SharedLevelData>("/root/SharedLevelData").LastLevel = ResourceLoader.Load<PackedScene>(this.SceneFilePath);
		GetNode<SharedLevelData>("/root/SharedLevelData").NextLevel = NextLevel;
		TotalMines = MinesToFlag;
		
		ThePlayer.ChangeMissionText(MissionText);
		
		if (CanUseSDFGI)
		{
			TheWorld.Environment.SdfgiEnabled = GetNode<SharedLevelData>("/root/SharedLevelData").SDFGIEnable;
		}
		
		TheWorld.Environment.SsilEnabled = GetNode<SharedLevelData>("/root/SharedLevelData").SSILEnable;
		TheWorld.Environment.SsaoEnabled = GetNode<SharedLevelData>("/root/SharedLevelData").SSAOEnable;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void UpdateScore()
	{
		MinesToFlag -= 1;
	}

	public void CorrectFlagRemoved()
	{
		MinesToFlag += 1;
	}

	public void GoToNextLevel()
	{
		if (MinesToFlag > 0)
		{
			GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").FailedScene);
			return;
		}
		
		GD.Print(CreateScore());
		GetNode<SharedLevelData>("/root/SharedLevelData").LevelScore = CreateScore();
		GetTree().ChangeSceneToPacked(GetNode<SharedLevelData>("/root/SharedLevelData").ScoreScene);
	}

	private int CreateScore()
	{
		if (FalseFlags <= 0)
		{
			return 100;
		}

		return (int) (((TotalMines) / (TotalMines + FalseFlags)) * 100.0f);
	}

	public void UpdateFalseFlag()
	{
		FalseFlags += 1;
	}

	public void FalseFlagRemoved()
	{
		FalseFlags -= 1;
	}

	public void UpdateSettings()
	{
		if (CanUseSDFGI)
		{
			TheWorld.Environment.SdfgiEnabled = GetNode<SharedLevelData>("/root/SharedLevelData").SDFGIEnable;
		}
		
		TheWorld.Environment.SsilEnabled = GetNode<SharedLevelData>("/root/SharedLevelData").SSILEnable;
		TheWorld.Environment.SsaoEnabled = GetNode<SharedLevelData>("/root/SharedLevelData").SSAOEnable;
	}
}
